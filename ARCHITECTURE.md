# Database Connection Architecture - Kiến trúc kết nối Database

## 1. TỔNG QUAN KIẾN TRÚC (Architecture Overview)

```
┌─────────────────────────────────────────────────────────────────┐
│                         Application Layer                        │
│                      (Your .NET Application)                     │
└────────────────┬────────────────────────────────────────────────┘
                 │
                 ├── ADO.NET (Low-level)
                 │   └── SqlConnection, SqlCommand
                 │
                 ├── Entity Framework Core (ORM)
                 │   └── DbContext, DbSet<T>
                 │
                 └── Dapper (Micro-ORM)
                     └── IDbConnection.Query<T>()
                 │
┌────────────────┴────────────────────────────────────────────────┐
│                      Database Providers                          │
│  (SqlClient, Npgsql, MySqlConnector, SQLite)                    │
└────────────────┬────────────────────────────────────────────────┘
                 │
┌────────────────┴────────────────────────────────────────────────┐
│                     Network/Protocol Layer                       │
│               (TCP/IP, SSL/TLS, Named Pipes)                     │
└────────────────┬────────────────────────────────────────────────┘
                 │
┌────────────────┴────────────────────────────────────────────────┐
│                        Database Server                           │
│  (SQL Server, PostgreSQL, MySQL, SQLite file)                   │
└─────────────────────────────────────────────────────────────────┘
```

## 2. CONNECTION POOLING

```
Application Threads          Connection Pool              Database
─────────────────           ────────────────            ───────────

Thread 1 ───────────┐       ┌─ [Conn 1] ──────────────► SQL Server
                    │       │
Thread 2 ───────────┼──────►├─ [Conn 2] ──────────────►
                    │       │
Thread 3 ───────────┘       ├─ [Conn 3] (idle)
                            │
                            ├─ [Conn 4] (idle)
                            │
                            └─ [Conn 5] (idle)

Pool Settings:
- Min Pool Size: 5
- Max Pool Size: 100
- Connection Lifetime: 300s
```

**Lợi ích:**
- Tái sử dụng connections
- Giảm overhead tạo connection mới
- Tự động quản lý lifecycle

## 3. ENTITY FRAMEWORK CORE ARCHITECTURE

```
┌──────────────────────────────────────────────────────────────┐
│                      Your Application                         │
│                                                               │
│  ┌────────────┐    ┌────────────┐    ┌────────────┐        │
│  │  Service   │───►│Controller  │───►│   View     │        │
│  │   Layer    │    │   Layer    │    │   Layer    │        │
│  └─────┬──────┘    └────────────┘    └────────────┘        │
└────────┼──────────────────────────────────────────────────────┘
         │
         ▼
┌──────────────────────────────────────────────────────────────┐
│                     DbContext                                 │
│                                                               │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │  DbSet<User> │  │ DbSet<Order> │  │ DbSet<...>   │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
│                                                               │
│  ┌──────────────────────────────────────────────────┐       │
│  │         Change Tracker                            │       │
│  │  (Tracks entity state: Added, Modified, etc.)    │       │
│  └──────────────────────────────────────────────────┘       │
│                                                               │
│  ┌──────────────────────────────────────────────────┐       │
│  │         LINQ Query Provider                       │       │
│  │  (Converts LINQ to SQL)                          │       │
│  └──────────────────────────────────────────────────┘       │
└────────────────────────┬─────────────────────────────────────┘
                         │
                         ▼
┌──────────────────────────────────────────────────────────────┐
│              Database Provider (SqlClient, etc.)              │
└────────────────────────┬─────────────────────────────────────┘
                         │
                         ▼
┌──────────────────────────────────────────────────────────────┐
│                     Database                                  │
└──────────────────────────────────────────────────────────────┘
```

## 4. QUERY EXECUTION FLOW

### ADO.NET Flow:
```
1. Create Connection
   ↓
2. Open Connection ────► Connection Pool ────► Database
   ↓
3. Create Command
   ↓
4. Add Parameters (prevent SQL injection)
   ↓
5. Execute Command ────► SQL Query ────► Database
   ↓
6. Read Results ◄────── Result Set ◄────── Database
   ↓
7. Close Connection ────► Return to Pool
```

### Entity Framework Core Flow:
```
1. LINQ Query
   context.Users.Where(u => u.Active).ToList()
   ↓
2. Query Translation
   LINQ → Expression Tree → SQL
   ↓
3. Query Caching (if enabled)
   ↓
4. Open Connection (from pool)
   ↓
5. Execute SQL ────► Database
   ↓
6. Materialize Results
   SQL Rows → Objects
   ↓
7. Change Tracking (if not AsNoTracking)
   ↓
8. Return Objects
   ↓
9. Close Connection (return to pool)
```

### Dapper Flow:
```
1. Create Connection
   ↓
2. Open Connection
   ↓
3. Execute Query with Dapper
   connection.Query<User>("SELECT * FROM Users WHERE Active = @Active", new { Active = true })
   ↓
4. Dapper Maps Results ────► Objects
   ↓
5. Return Objects
   ↓
6. Close Connection
```

## 5. TRANSACTION FLOW

```
┌─────────────────────────────────────────────────────────────┐
│                    Application                               │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       │ BeginTransaction()
                       ▼
┌─────────────────────────────────────────────────────────────┐
│                    Transaction                               │
│                                                              │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐     │
│  │ Operation 1  │  │ Operation 2  │  │ Operation 3  │     │
│  │  (Insert)    │  │  (Update)    │  │  (Delete)    │     │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘     │
│         │                  │                  │              │
│         └──────────────────┴──────────────────┘              │
│                            │                                 │
│                   ┌────────┴────────┐                       │
│                   │                 │                        │
│                Success?          Error?                      │
│                   │                 │                        │
│              ┌────▼────┐      ┌────▼────┐                  │
│              │ Commit  │      │Rollback │                   │
│              └────┬────┘      └────┬────┘                  │
└───────────────────┼──────────────────┼──────────────────────┘
                    │                  │
                    ▼                  ▼
            ┌──────────────┐    ┌──────────────┐
            │   Changes    │    │  No Changes  │
            │   Saved      │    │  (Reverted)  │
            └──────────────┘    └──────────────┘
```

## 6. SECURITY LAYERS

```
┌─────────────────────────────────────────────────────────────┐
│  Layer 1: Input Validation                                   │
│  ✓ Validate user input                                       │
│  ✓ Check data types and formats                             │
└────────────────────────┬────────────────────────────────────┘
                         ▼
┌─────────────────────────────────────────────────────────────┐
│  Layer 2: Parameterized Queries                             │
│  ✓ Use parameters instead of string concatenation           │
│  ✓ Prevents SQL Injection                                   │
└────────────────────────┬────────────────────────────────────┘
                         ▼
┌─────────────────────────────────────────────────────────────┐
│  Layer 3: Connection Security                               │
│  ✓ Encrypted connection string                              │
│  ✓ SSL/TLS encryption                                       │
│  ✓ Least privilege principle                                │
└────────────────────────┬────────────────────────────────────┘
                         ▼
┌─────────────────────────────────────────────────────────────┐
│  Layer 4: Network Security                                  │
│  ✓ Firewall rules                                           │
│  ✓ VPN/Private network                                      │
└────────────────────────┬────────────────────────────────────┘
                         ▼
┌─────────────────────────────────────────────────────────────┐
│  Layer 5: Database Security                                 │
│  ✓ Database authentication                                  │
│  ✓ Row-level security                                       │
│  ✓ Encrypted data at rest                                   │
└─────────────────────────────────────────────────────────────┘
```

## 7. PERFORMANCE OPTIMIZATION STRATEGIES

```
Strategy 1: Connection Pooling
─────────────────────────────
Single Connection:  ████████████████████ 100% overhead
Pooled (10 conns): █ 10% overhead

Strategy 2: AsNoTracking
─────────────────────────────
With Tracking:    ████████████ 100% time
AsNoTracking:     ██████ 50% time

Strategy 3: Select Only Needed Columns
─────────────────────────────────────
Select *:         ████████████ Transfer all data
Select Id, Name:  ███ Transfer only needed

Strategy 4: Pagination
─────────────────────────────
All Records:      ████████████████████ 10,000 rows
Paginated (100):  █ 100 rows per query

Strategy 5: Indexing
─────────────────────────────
No Index:         ████████████████████ Full table scan
With Index:       ██ Index seek
```

## 8. ERROR HANDLING FLOW

```
                    Try Connection
                         │
                         ▼
                   ┌──────────┐
                   │ Success? │
                   └─────┬────┘
                         │
           ┌─────────────┴─────────────┐
           │                           │
        Success                     Failure
           │                           │
           ▼                           ▼
    Execute Query              ┌──────────────┐
           │                   │ Error Type?  │
           │                   └──────┬───────┘
           │                          │
           ▼              ┌────────────┼────────────┐
    ┌──────────┐          │            │            │
    │ Success? │    Timeout    Connection    SQL Error
    └─────┬────┘          │      Error         │
          │               │            │            │
    ┌─────┴─────┐         ▼            ▼            ▼
    │           │    Retry?      Reconnect?   Log & Handle
 Success    Exception      │            │            │
    │           │          ▼            ▼            ▼
    ▼           ▼     Increase    Check Service  User-friendly
 Return    Log Error   Timeout    & Firewall     Message
 Result    & Handle
```

## 9. DEPLOYMENT ARCHITECTURE

### Development:
```
Developer Machine
    │
    ├─ Local SQL Server (localhost)
    └─ SQLite file (for testing)
```

### Staging:
```
Application Server ──► Staging Database Server
    │                       │
    ├─ Connection Pool     ├─ Test Data
    └─ Read-only User      └─ Regular Backups
```

### Production:
```
┌──────────────────┐
│  Load Balancer   │
└────────┬─────────┘
         │
    ┌────┴────┐
    │         │
┌───▼───┐ ┌──▼────┐        ┌──────────────────┐
│ App 1 │ │ App 2 │───────►│ Primary Database │
└───────┘ └───────┘        └────────┬─────────┘
                                     │
                            ┌────────┴────────┐
                            │                 │
                      ┌─────▼──────┐   ┌─────▼──────┐
                      │  Replica 1 │   │  Replica 2 │
                      │  (Read)    │   │  (Read)    │
                      └────────────┘   └────────────┘
```

## 10. MONITORING & LOGGING

```
┌─────────────────────────────────────────────────────────────┐
│                      Application                             │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐                 │
│  │ Logging  │  │ Metrics  │  │ Tracing  │                 │
│  └────┬─────┘  └────┬─────┘  └────┬─────┘                 │
└───────┼─────────────┼─────────────┼─────────────────────────┘
        │             │             │
        ▼             ▼             ▼
┌─────────────┐ ┌─────────────┐ ┌─────────────┐
│ Log Files   │ │ Prometheus  │ │   Zipkin    │
│ (Serilog)   │ │ (Metrics)   │ │  (Tracing)  │
└─────────────┘ └─────────────┘ └─────────────┘
        │             │             │
        └─────────────┴─────────────┘
                      │
                      ▼
        ┌──────────────────────────┐
        │   Monitoring Dashboard   │
        │  (Grafana, ELK Stack)    │
        └──────────────────────────┘
```

**Metrics to Monitor:**
- Connection pool usage
- Query execution time
- Failed connections
- Deadlocks
- Long-running queries
- CPU & Memory usage

## SUMMARY

Kiến trúc database connection trong .NET/C# bao gồm nhiều layers:
1. **Application Layer** - Code của bạn
2. **ORM/Data Access Layer** - EF Core, Dapper, ADO.NET
3. **Provider Layer** - Database-specific drivers
4. **Network Layer** - TCP/IP, SSL/TLS
5. **Database Layer** - SQL Server, MySQL, PostgreSQL, etc.

Mỗi layer có vai trò riêng và cần được cấu hình đúng để đảm bảo:
- **Performance** - Nhanh và hiệu quả
- **Security** - An toàn và bảo mật
- **Reliability** - Ổn định và đáng tin cậy
- **Scalability** - Có thể mở rộng
