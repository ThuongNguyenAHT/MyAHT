; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [331 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [656 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 69
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 68
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 109
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 248
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 282
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 49
	i32 39485524, ; 6: System.Net.WebSockets.dll => 0x25a8054 => 81
	i32 42639949, ; 7: System.Threading.Thread => 0x28aa24d => 146
	i32 66541672, ; 8: System.Diagnostics.StackTrace => 0x3f75868 => 31
	i32 67008169, ; 9: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 323
	i32 68219467, ; 10: System.Security.Cryptography.Primitives => 0x410f24b => 125
	i32 72070932, ; 11: Microsoft.Maui.Graphics.dll => 0x44bb714 => 205
	i32 82292897, ; 12: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 103
	i32 101534019, ; 13: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 266
	i32 117431740, ; 14: System.Runtime.InteropServices => 0x6ffddbc => 108
	i32 120558881, ; 15: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 266
	i32 122350210, ; 16: System.Threading.Channels.dll => 0x74aea82 => 140
	i32 134690465, ; 17: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 286
	i32 142721839, ; 18: System.Net.WebHeaderCollection => 0x881c32f => 78
	i32 149972175, ; 19: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 125
	i32 159306688, ; 20: System.ComponentModel.Annotations => 0x97ed3c0 => 14
	i32 165246403, ; 21: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 222
	i32 176265551, ; 22: System.ServiceProcess => 0xa81994f => 133
	i32 182336117, ; 23: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 268
	i32 184328833, ; 24: System.ValueTuple.dll => 0xafca281 => 152
	i32 190611925, ; 25: en/MyCompany.resources.dll => 0xb5c81d5 => 0
	i32 195452805, ; 26: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 320
	i32 199333315, ; 27: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 321
	i32 205061960, ; 28: System.ComponentModel => 0xc38ff48 => 19
	i32 209399409, ; 29: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 220
	i32 220171995, ; 30: System.Diagnostics.Debug => 0xd1f8edb => 27
	i32 230216969, ; 31: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 242
	i32 230752869, ; 32: Microsoft.CSharp.dll => 0xdc10265 => 2
	i32 231409092, ; 33: System.Linq.Parallel => 0xdcb05c4 => 60
	i32 231814094, ; 34: System.Globalization => 0xdd133ce => 43
	i32 246610117, ; 35: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 92
	i32 254259026, ; 36: Microsoft.AspNetCore.Components.dll => 0xf27af52 => 176
	i32 261689757, ; 37: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 225
	i32 276479776, ; 38: System.Threading.Timer.dll => 0x107abf20 => 148
	i32 278686392, ; 39: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 244
	i32 280482487, ; 40: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 241
	i32 280992041, ; 41: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 292
	i32 291076382, ; 42: System.IO.Pipes.AccessControl.dll => 0x1159791e => 55
	i32 298918909, ; 43: System.Net.Ping.dll => 0x11d123fd => 70
	i32 317674968, ; 44: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 320
	i32 318968648, ; 45: Xamarin.AndroidX.Activity.dll => 0x13031348 => 211
	i32 321597661, ; 46: System.Numerics => 0x132b30dd => 84
	i32 336156722, ; 47: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 305
	i32 342366114, ; 48: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 243
	i32 356389973, ; 49: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 304
	i32 360082299, ; 50: System.ServiceModel.Web => 0x15766b7b => 132
	i32 367780167, ; 51: System.IO.Pipes => 0x15ebe147 => 56
	i32 374914964, ; 52: System.Transactions.Local => 0x1658bf94 => 150
	i32 375677976, ; 53: System.Net.ServicePoint.dll => 0x16646418 => 75
	i32 379916513, ; 54: System.Threading.Thread.dll => 0x16a510e1 => 146
	i32 385762202, ; 55: System.Memory.dll => 0x16fe439a => 63
	i32 392610295, ; 56: System.Threading.ThreadPool.dll => 0x1766c1f7 => 147
	i32 395744057, ; 57: _Microsoft.Android.Resource.Designer => 0x17969339 => 327
	i32 403441872, ; 58: WindowsBase => 0x180c08d0 => 166
	i32 435591531, ; 59: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 316
	i32 441335492, ; 60: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 226
	i32 442565967, ; 61: System.Collections => 0x1a61054f => 13
	i32 450948140, ; 62: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 239
	i32 451504562, ; 63: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 126
	i32 456227837, ; 64: System.Web.HttpUtility.dll => 0x1b317bfd => 153
	i32 459347974, ; 65: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 114
	i32 465846621, ; 66: mscorlib => 0x1bc4415d => 167
	i32 469710990, ; 67: System.dll => 0x1bff388e => 165
	i32 476646585, ; 68: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 241
	i32 486930444, ; 69: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 254
	i32 498788369, ; 70: System.ObjectModel => 0x1dbae811 => 85
	i32 500358224, ; 71: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 303
	i32 503918385, ; 72: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 297
	i32 513247710, ; 73: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 198
	i32 526420162, ; 74: System.Transactions.dll => 0x1f6088c2 => 151
	i32 527452488, ; 75: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 286
	i32 530272170, ; 76: System.Linq.Queryable => 0x1f9b4faa => 61
	i32 539058512, ; 77: Microsoft.Extensions.Logging => 0x20216150 => 195
	i32 540030774, ; 78: System.IO.FileSystem.dll => 0x20303736 => 52
	i32 545304856, ; 79: System.Runtime.Extensions => 0x2080b118 => 104
	i32 546455878, ; 80: System.Runtime.Serialization.Xml => 0x20924146 => 115
	i32 549171840, ; 81: System.Globalization.Calendars => 0x20bbb280 => 41
	i32 557405415, ; 82: Jsr305Binding => 0x213954e7 => 279
	i32 569601784, ; 83: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 277
	i32 571435654, ; 84: Microsoft.Extensions.FileProviders.Embedded.dll => 0x220f6a86 => 191
	i32 577335427, ; 85: System.Security.Cryptography.Cng => 0x22697083 => 121
	i32 592146354, ; 86: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 311
	i32 601371474, ; 87: System.IO.IsolatedStorage.dll => 0x23d83352 => 53
	i32 605376203, ; 88: System.IO.Compression.FileSystem => 0x24154ecb => 45
	i32 613668793, ; 89: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 120
	i32 627609679, ; 90: Xamarin.AndroidX.CustomView => 0x2568904f => 231
	i32 627931235, ; 91: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 309
	i32 639843206, ; 92: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 237
	i32 643868501, ; 93: System.Net => 0x2660a755 => 82
	i32 662205335, ; 94: System.Text.Encodings.Web.dll => 0x27787397 => 137
	i32 663517072, ; 95: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 273
	i32 666292255, ; 96: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 218
	i32 672442732, ; 97: System.Collections.Concurrent => 0x2814a96c => 9
	i32 683518922, ; 98: System.Net.Security => 0x28bdabca => 74
	i32 688181140, ; 99: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 291
	i32 690569205, ; 100: System.Xml.Linq.dll => 0x29293ff5 => 156
	i32 691348768, ; 101: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 288
	i32 693804605, ; 102: System.Windows => 0x295a9e3d => 155
	i32 699345723, ; 103: System.Reflection.Emit => 0x29af2b3b => 93
	i32 700284507, ; 104: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 283
	i32 700358131, ; 105: System.IO.Compression.ZipFile => 0x29be9df3 => 46
	i32 706645707, ; 106: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 306
	i32 709557578, ; 107: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 294
	i32 720511267, ; 108: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 287
	i32 722857257, ; 109: System.Runtime.Loader.dll => 0x2b15ed29 => 110
	i32 735137430, ; 110: System.Security.SecureString.dll => 0x2bd14e96 => 130
	i32 752232764, ; 111: System.Diagnostics.Contracts.dll => 0x2cd6293c => 26
	i32 755313932, ; 112: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 208
	i32 759454413, ; 113: System.Net.Requests => 0x2d445acd => 73
	i32 762598435, ; 114: System.IO.Pipes.dll => 0x2d745423 => 56
	i32 775507847, ; 115: System.IO.Compression => 0x2e394f87 => 47
	i32 777317022, ; 116: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 315
	i32 789151979, ; 117: Microsoft.Extensions.Options => 0x2f0980eb => 197
	i32 790371945, ; 118: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 232
	i32 804008546, ; 119: Microsoft.AspNetCore.Components.WebView.Maui => 0x2fec3262 => 180
	i32 804715423, ; 120: System.Data.Common => 0x2ff6fb9f => 23
	i32 807930345, ; 121: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 246
	i32 823281589, ; 122: System.Private.Uri.dll => 0x311247b5 => 87
	i32 830298997, ; 123: System.IO.Compression.Brotli => 0x317d5b75 => 44
	i32 832635846, ; 124: System.Xml.XPath.dll => 0x31a103c6 => 161
	i32 834051424, ; 125: System.Net.Quic => 0x31b69d60 => 72
	i32 843511501, ; 126: Xamarin.AndroidX.Print => 0x3246f6cd => 259
	i32 873119928, ; 127: Microsoft.VisualBasic => 0x340ac0b8 => 4
	i32 877678880, ; 128: System.Globalization.dll => 0x34505120 => 43
	i32 878954865, ; 129: System.Net.Http.Json => 0x3463c971 => 64
	i32 904024072, ; 130: System.ComponentModel.Primitives.dll => 0x35e25008 => 17
	i32 911108515, ; 131: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 54
	i32 926902833, ; 132: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 318
	i32 928116545, ; 133: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 282
	i32 952186615, ; 134: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 106
	i32 956575887, ; 135: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 287
	i32 966729478, ; 136: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 280
	i32 967690846, ; 137: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 243
	i32 975236339, ; 138: System.Diagnostics.Tracing => 0x3a20ecf3 => 35
	i32 975874589, ; 139: System.Xml.XDocument => 0x3a2aaa1d => 159
	i32 986514023, ; 140: System.Private.DataContractSerialization.dll => 0x3acd0267 => 86
	i32 987214855, ; 141: System.Diagnostics.Tools => 0x3ad7b407 => 33
	i32 992768348, ; 142: System.Collections.dll => 0x3b2c715c => 13
	i32 994442037, ; 143: System.IO.FileSystem => 0x3b45fb35 => 52
	i32 999186168, ; 144: Microsoft.Extensions.FileSystemGlobbing.dll => 0x3b8e5ef8 => 193
	i32 1001831731, ; 145: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 57
	i32 1012816738, ; 146: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 263
	i32 1019214401, ; 147: System.Drawing => 0x3cbffa41 => 37
	i32 1028951442, ; 148: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 188
	i32 1029334545, ; 149: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 293
	i32 1031528504, ; 150: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 281
	i32 1035644815, ; 151: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 216
	i32 1036536393, ; 152: System.Drawing.Primitives.dll => 0x3dc84a49 => 36
	i32 1044663988, ; 153: System.Linq.Expressions.dll => 0x3e444eb4 => 59
	i32 1052210849, ; 154: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 250
	i32 1067306892, ; 155: GoogleGson => 0x3f9dcf8c => 174
	i32 1082857460, ; 156: System.ComponentModel.TypeConverter => 0x408b17f4 => 18
	i32 1084122840, ; 157: Xamarin.Kotlin.StdLib => 0x409e66d8 => 284
	i32 1098259244, ; 158: System => 0x41761b2c => 165
	i32 1106973742, ; 159: Microsoft.Extensions.Configuration.FileExtensions.dll => 0x41fb142e => 185
	i32 1118262833, ; 160: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 306
	i32 1121599056, ; 161: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 249
	i32 1149092582, ; 162: Xamarin.AndroidX.Window => 0x447dc2e6 => 276
	i32 1168523401, ; 163: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 312
	i32 1170634674, ; 164: System.Web.dll => 0x45c677b2 => 154
	i32 1173126369, ; 165: Microsoft.Extensions.FileProviders.Abstractions.dll => 0x45ec7ce1 => 189
	i32 1175144683, ; 166: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 272
	i32 1178241025, ; 167: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 257
	i32 1203215381, ; 168: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 310
	i32 1204270330, ; 169: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 218
	i32 1208641965, ; 170: System.Diagnostics.Process => 0x480a69ad => 30
	i32 1219128291, ; 171: System.IO.IsolatedStorage => 0x48aa6be3 => 53
	i32 1234928153, ; 172: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 308
	i32 1243150071, ; 173: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 277
	i32 1253011324, ; 174: Microsoft.Win32.Registry => 0x4aaf6f7c => 6
	i32 1260983243, ; 175: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 292
	i32 1264511973, ; 176: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 267
	i32 1266063107, ; 177: en\MyCompany.resources => 0x4b769703 => 0
	i32 1267360935, ; 178: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 271
	i32 1273260888, ; 179: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 223
	i32 1275534314, ; 180: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 288
	i32 1278448581, ; 181: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 215
	i32 1293217323, ; 182: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 234
	i32 1309188875, ; 183: System.Private.DataContractSerialization => 0x4e08a30b => 86
	i32 1322716291, ; 184: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 276
	i32 1324164729, ; 185: System.Linq => 0x4eed2679 => 62
	i32 1335329327, ; 186: System.Runtime.Serialization.Json.dll => 0x4f97822f => 113
	i32 1364015309, ; 187: System.IO => 0x514d38cd => 58
	i32 1373134921, ; 188: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 322
	i32 1376866003, ; 189: Xamarin.AndroidX.SavedState => 0x52114ed3 => 263
	i32 1379779777, ; 190: System.Resources.ResourceManager => 0x523dc4c1 => 100
	i32 1402170036, ; 191: System.Configuration.dll => 0x53936ab4 => 20
	i32 1406073936, ; 192: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 227
	i32 1408764838, ; 193: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 112
	i32 1411638395, ; 194: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 102
	i32 1422545099, ; 195: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 103
	i32 1430672901, ; 196: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 290
	i32 1434145427, ; 197: System.Runtime.Handles => 0x557b5293 => 105
	i32 1435222561, ; 198: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 280
	i32 1439761251, ; 199: System.Net.Quic.dll => 0x55d10363 => 72
	i32 1452070440, ; 200: System.Formats.Asn1.dll => 0x568cd628 => 39
	i32 1453312822, ; 201: System.Diagnostics.Tools.dll => 0x569fcb36 => 33
	i32 1454105418, ; 202: Microsoft.Extensions.FileProviders.Composite => 0x56abe34a => 190
	i32 1457743152, ; 203: System.Runtime.Extensions.dll => 0x56e36530 => 104
	i32 1458022317, ; 204: System.Net.Security.dll => 0x56e7a7ad => 74
	i32 1461004990, ; 205: es\Microsoft.Maui.Controls.resources => 0x57152abe => 296
	i32 1461234159, ; 206: System.Collections.Immutable.dll => 0x5718a9ef => 10
	i32 1461719063, ; 207: System.Security.Cryptography.OpenSsl => 0x57201017 => 124
	i32 1462112819, ; 208: System.IO.Compression.dll => 0x57261233 => 47
	i32 1469204771, ; 209: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 217
	i32 1470490898, ; 210: Microsoft.Extensions.Primitives => 0x57a5e912 => 198
	i32 1479771757, ; 211: System.Collections.Immutable => 0x5833866d => 10
	i32 1480492111, ; 212: System.IO.Compression.Brotli.dll => 0x583e844f => 44
	i32 1487239319, ; 213: Microsoft.Win32.Primitives => 0x58a57897 => 5
	i32 1490025113, ; 214: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 264
	i32 1493001747, ; 215: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 300
	i32 1514721132, ; 216: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 295
	i32 1521091094, ; 217: Microsoft.Extensions.FileSystemGlobbing => 0x5aaa0216 => 193
	i32 1536373174, ; 218: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 32
	i32 1543031311, ; 219: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 139
	i32 1543355203, ; 220: System.Reflection.Emit.dll => 0x5bfdbb43 => 93
	i32 1546581739, ; 221: Microsoft.AspNetCore.Components.WebView.Maui.dll => 0x5c2ef6eb => 180
	i32 1550322496, ; 222: System.Reflection.Extensions.dll => 0x5c680b40 => 94
	i32 1551623176, ; 223: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 315
	i32 1565862583, ; 224: System.IO.FileSystem.Primitives => 0x5d552ab7 => 50
	i32 1566207040, ; 225: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 142
	i32 1573704789, ; 226: System.Runtime.Serialization.Json => 0x5dccd455 => 113
	i32 1580037396, ; 227: System.Threading.Overlapped => 0x5e2d7514 => 141
	i32 1582372066, ; 228: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 233
	i32 1592978981, ; 229: System.Runtime.Serialization.dll => 0x5ef2ee25 => 116
	i32 1597949149, ; 230: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 281
	i32 1601112923, ; 231: System.Xml.Serialization => 0x5f6f0b5b => 158
	i32 1603525486, ; 232: Microsoft.Maui.Controls.HotReload.Forms.dll => 0x5f93db6e => 324
	i32 1604827217, ; 233: System.Net.WebClient => 0x5fa7b851 => 77
	i32 1618516317, ; 234: System.Net.WebSockets.Client.dll => 0x6078995d => 80
	i32 1622152042, ; 235: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 253
	i32 1622358360, ; 236: System.Dynamic.Runtime => 0x60b33958 => 38
	i32 1624863272, ; 237: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 275
	i32 1632842087, ; 238: Microsoft.Extensions.Configuration.Json => 0x61533167 => 186
	i32 1635184631, ; 239: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 237
	i32 1636350590, ; 240: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 230
	i32 1639515021, ; 241: System.Net.Http.dll => 0x61b9038d => 65
	i32 1639986890, ; 242: System.Text.RegularExpressions => 0x61c036ca => 139
	i32 1641389582, ; 243: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 16
	i32 1654881142, ; 244: Microsoft.AspNetCore.Components.WebView => 0x62a37b76 => 179
	i32 1657153582, ; 245: System.Runtime => 0x62c6282e => 117
	i32 1658241508, ; 246: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 269
	i32 1658251792, ; 247: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 278
	i32 1670060433, ; 248: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 225
	i32 1675553242, ; 249: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 49
	i32 1677501392, ; 250: System.Net.Primitives.dll => 0x63fca3d0 => 71
	i32 1678508291, ; 251: System.Net.WebSockets => 0x640c0103 => 81
	i32 1679769178, ; 252: System.Security.Cryptography => 0x641f3e5a => 127
	i32 1691477237, ; 253: System.Reflection.Metadata => 0x64d1e4f5 => 95
	i32 1696967625, ; 254: System.Security.Cryptography.Csp => 0x6525abc9 => 122
	i32 1698840827, ; 255: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 285
	i32 1701541528, ; 256: System.Diagnostics.Debug.dll => 0x656b7698 => 27
	i32 1720223769, ; 257: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 246
	i32 1726116996, ; 258: System.Reflection.dll => 0x66e27484 => 98
	i32 1728033016, ; 259: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 29
	i32 1729485958, ; 260: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 221
	i32 1736233607, ; 261: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 313
	i32 1743415430, ; 262: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 291
	i32 1744735666, ; 263: System.Transactions.Local.dll => 0x67fe8db2 => 150
	i32 1746115085, ; 264: System.IO.Pipelines.dll => 0x68139a0d => 206
	i32 1746316138, ; 265: Mono.Android.Export => 0x6816ab6a => 170
	i32 1750313021, ; 266: Microsoft.Win32.Primitives.dll => 0x6853a83d => 5
	i32 1758240030, ; 267: System.Resources.Reader.dll => 0x68cc9d1e => 99
	i32 1760259689, ; 268: Microsoft.AspNetCore.Components.Web.dll => 0x68eb6e69 => 178
	i32 1763938596, ; 269: System.Diagnostics.TraceSource.dll => 0x69239124 => 34
	i32 1765942094, ; 270: System.Reflection.Extensions => 0x6942234e => 94
	i32 1766324549, ; 271: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 268
	i32 1770582343, ; 272: Microsoft.Extensions.Logging.dll => 0x6988f147 => 195
	i32 1776026572, ; 273: System.Core.dll => 0x69dc03cc => 22
	i32 1777075843, ; 274: System.Globalization.Extensions.dll => 0x69ec0683 => 42
	i32 1780572499, ; 275: Mono.Android.Runtime.dll => 0x6a216153 => 171
	i32 1782862114, ; 276: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 307
	i32 1788241197, ; 277: Xamarin.AndroidX.Fragment => 0x6a96652d => 239
	i32 1793755602, ; 278: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 299
	i32 1808609942, ; 279: Xamarin.AndroidX.Loader => 0x6bcd3296 => 253
	i32 1813058853, ; 280: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 284
	i32 1813201214, ; 281: Xamarin.Google.Android.Material => 0x6c13413e => 278
	i32 1818569960, ; 282: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 258
	i32 1818787751, ; 283: Microsoft.VisualBasic.Core => 0x6c687fa7 => 3
	i32 1824175904, ; 284: System.Text.Encoding.Extensions => 0x6cbab720 => 135
	i32 1824722060, ; 285: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 112
	i32 1827303595, ; 286: Microsoft.VisualStudio.DesignTools.TapContract => 0x6cea70ab => 326
	i32 1828688058, ; 287: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 196
	i32 1842015223, ; 288: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 319
	i32 1847515442, ; 289: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 208
	i32 1853025655, ; 290: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 316
	i32 1858542181, ; 291: System.Linq.Expressions => 0x6ec71a65 => 59
	i32 1870277092, ; 292: System.Reflection.Primitives => 0x6f7a29e4 => 96
	i32 1875935024, ; 293: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 298
	i32 1879696579, ; 294: System.Formats.Tar.dll => 0x7009e4c3 => 40
	i32 1885316902, ; 295: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 219
	i32 1885918049, ; 296: Microsoft.VisualStudio.DesignTools.TapContract.dll => 0x7068d361 => 326
	i32 1888955245, ; 297: System.Diagnostics.Contracts => 0x70972b6d => 26
	i32 1889954781, ; 298: System.Reflection.Metadata.dll => 0x70a66bdd => 95
	i32 1898237753, ; 299: System.Reflection.DispatchProxy => 0x7124cf39 => 90
	i32 1900610850, ; 300: System.Resources.ResourceManager.dll => 0x71490522 => 100
	i32 1910275211, ; 301: System.Collections.NonGeneric.dll => 0x71dc7c8b => 11
	i32 1939592360, ; 302: System.Private.Xml.Linq => 0x739bd4a8 => 88
	i32 1956758971, ; 303: System.Resources.Writer => 0x74a1c5bb => 101
	i32 1961813231, ; 304: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 265
	i32 1968388702, ; 305: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 182
	i32 1983156543, ; 306: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 285
	i32 1985761444, ; 307: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 210
	i32 2003115576, ; 308: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 295
	i32 2011961780, ; 309: System.Buffers.dll => 0x77ec19b4 => 8
	i32 2019465201, ; 310: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 250
	i32 2025202353, ; 311: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 290
	i32 2031763787, ; 312: Xamarin.Android.Glide => 0x791a414b => 207
	i32 2045470958, ; 313: System.Private.Xml => 0x79eb68ee => 89
	i32 2048278909, ; 314: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 184
	i32 2055257422, ; 315: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 245
	i32 2060060697, ; 316: System.Windows.dll => 0x7aca0819 => 155
	i32 2066184531, ; 317: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 294
	i32 2070888862, ; 318: System.Diagnostics.TraceSource => 0x7b6f419e => 34
	i32 2072397586, ; 319: Microsoft.Extensions.FileProviders.Physical => 0x7b864712 => 192
	i32 2079903147, ; 320: System.Runtime.dll => 0x7bf8cdab => 117
	i32 2090596640, ; 321: System.Numerics.Vectors => 0x7c9bf920 => 83
	i32 2127167465, ; 322: System.Console => 0x7ec9ffe9 => 21
	i32 2142473426, ; 323: System.Collections.Specialized => 0x7fb38cd2 => 12
	i32 2143790110, ; 324: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 163
	i32 2146852085, ; 325: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 4
	i32 2159891885, ; 326: Microsoft.Maui => 0x80bd55ad => 203
	i32 2169148018, ; 327: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 302
	i32 2181898931, ; 328: Microsoft.Extensions.Options.dll => 0x820d22b3 => 197
	i32 2192057212, ; 329: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 196
	i32 2193016926, ; 330: System.ObjectModel.dll => 0x82b6c85e => 85
	i32 2201107256, ; 331: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 289
	i32 2201231467, ; 332: System.Net.Http => 0x8334206b => 65
	i32 2207618523, ; 333: it\Microsoft.Maui.Controls.resources => 0x839595db => 304
	i32 2217644978, ; 334: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 272
	i32 2222056684, ; 335: System.Threading.Tasks.Parallel => 0x8471e4ec => 144
	i32 2244775296, ; 336: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 254
	i32 2252106437, ; 337: System.Xml.Serialization.dll => 0x863c6ac5 => 158
	i32 2256313426, ; 338: System.Globalization.Extensions => 0x867c9c52 => 42
	i32 2265110946, ; 339: System.Security.AccessControl.dll => 0x8702d9a2 => 118
	i32 2266799131, ; 340: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 183
	i32 2267999099, ; 341: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 209
	i32 2270573516, ; 342: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 298
	i32 2279755925, ; 343: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 261
	i32 2293034957, ; 344: System.ServiceModel.Web.dll => 0x88acefcd => 132
	i32 2295906218, ; 345: System.Net.Sockets => 0x88d8bfaa => 76
	i32 2298471582, ; 346: System.Net.Mail => 0x88ffe49e => 67
	i32 2303942373, ; 347: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 308
	i32 2305521784, ; 348: System.Private.CoreLib.dll => 0x896b7878 => 173
	i32 2315684594, ; 349: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 213
	i32 2320631194, ; 350: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 144
	i32 2340441535, ; 351: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 107
	i32 2344264397, ; 352: System.ValueTuple => 0x8bbaa2cd => 152
	i32 2353062107, ; 353: System.Net.Primitives => 0x8c40e0db => 71
	i32 2368005991, ; 354: System.Xml.ReaderWriter.dll => 0x8d24e767 => 157
	i32 2371007202, ; 355: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 182
	i32 2378619854, ; 356: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 122
	i32 2383496789, ; 357: System.Security.Principal.Windows.dll => 0x8e114655 => 128
	i32 2395872292, ; 358: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 303
	i32 2401565422, ; 359: System.Web.HttpUtility => 0x8f24faee => 153
	i32 2403452196, ; 360: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 236
	i32 2409983638, ; 361: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 0x8fa56e96 => 325
	i32 2411328690, ; 362: Microsoft.AspNetCore.Components => 0x8fb9f4b2 => 176
	i32 2421380589, ; 363: System.Threading.Tasks.Dataflow => 0x905355ed => 142
	i32 2423080555, ; 364: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 223
	i32 2427813419, ; 365: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 300
	i32 2435356389, ; 366: System.Console.dll => 0x912896e5 => 21
	i32 2435904999, ; 367: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 15
	i32 2442556106, ; 368: Microsoft.JSInterop.dll => 0x919672ca => 199
	i32 2454642406, ; 369: System.Text.Encoding.dll => 0x924edee6 => 136
	i32 2458678730, ; 370: System.Net.Sockets.dll => 0x928c75ca => 76
	i32 2459001652, ; 371: System.Linq.Parallel.dll => 0x92916334 => 60
	i32 2465532216, ; 372: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 226
	i32 2471841756, ; 373: netstandard.dll => 0x93554fdc => 168
	i32 2475788418, ; 374: Java.Interop.dll => 0x93918882 => 169
	i32 2480646305, ; 375: Microsoft.Maui.Controls => 0x93dba8a1 => 201
	i32 2483903535, ; 376: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 16
	i32 2484371297, ; 377: System.Net.ServicePoint => 0x94147f61 => 75
	i32 2489677290, ; 378: MyCompany => 0x946575ea => 1
	i32 2490993605, ; 379: System.AppContext.dll => 0x94798bc5 => 7
	i32 2501346920, ; 380: System.Data.DataSetExtensions => 0x95178668 => 24
	i32 2505896520, ; 381: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 248
	i32 2522472828, ; 382: Xamarin.Android.Glide.dll => 0x9659e17c => 207
	i32 2537015816, ; 383: Microsoft.AspNetCore.Authorization => 0x9737ca08 => 175
	i32 2538310050, ; 384: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 92
	i32 2550873716, ; 385: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 301
	i32 2562349572, ; 386: Microsoft.CSharp => 0x98ba5a04 => 2
	i32 2570120770, ; 387: System.Text.Encodings.Web => 0x9930ee42 => 137
	i32 2581783588, ; 388: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 249
	i32 2581819634, ; 389: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 271
	i32 2585220780, ; 390: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 135
	i32 2585805581, ; 391: System.Net.Ping => 0x9a20430d => 70
	i32 2585813321, ; 392: Microsoft.AspNetCore.Components.Forms => 0x9a206149 => 177
	i32 2589602615, ; 393: System.Threading.ThreadPool => 0x9a5a3337 => 147
	i32 2592341985, ; 394: Microsoft.Extensions.FileProviders.Abstractions => 0x9a83ffe1 => 189
	i32 2593496499, ; 395: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 310
	i32 2605712449, ; 396: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 289
	i32 2615233544, ; 397: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 240
	i32 2617129537, ; 398: System.Private.Xml.dll => 0x9bfe3a41 => 89
	i32 2618712057, ; 399: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 97
	i32 2620871830, ; 400: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 230
	i32 2624644809, ; 401: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 235
	i32 2626831493, ; 402: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 305
	i32 2627185994, ; 403: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 32
	i32 2629843544, ; 404: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 46
	i32 2633051222, ; 405: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 244
	i32 2663391936, ; 406: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 209
	i32 2663698177, ; 407: System.Runtime.Loader => 0x9ec4cf01 => 110
	i32 2664396074, ; 408: System.Xml.XDocument.dll => 0x9ecf752a => 159
	i32 2665622720, ; 409: System.Drawing.Primitives => 0x9ee22cc0 => 36
	i32 2676780864, ; 410: System.Data.Common.dll => 0x9f8c6f40 => 23
	i32 2686887180, ; 411: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 115
	i32 2692077919, ; 412: Microsoft.AspNetCore.Components.WebView.dll => 0xa075d95f => 179
	i32 2693849962, ; 413: System.IO.dll => 0xa090e36a => 58
	i32 2701096212, ; 414: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 269
	i32 2715334215, ; 415: System.Threading.Tasks.dll => 0xa1d8b647 => 145
	i32 2717744543, ; 416: System.Security.Claims => 0xa1fd7d9f => 119
	i32 2719963679, ; 417: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 121
	i32 2724373263, ; 418: System.Runtime.Numerics.dll => 0xa262a30f => 111
	i32 2732626843, ; 419: Xamarin.AndroidX.Activity => 0xa2e0939b => 211
	i32 2735172069, ; 420: System.Threading.Channels => 0xa30769e5 => 140
	i32 2735631878, ; 421: Microsoft.AspNetCore.Authorization.dll => 0xa30e6e06 => 175
	i32 2737747696, ; 422: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 217
	i32 2740948882, ; 423: System.IO.Pipes.AccessControl => 0xa35f8f92 => 55
	i32 2748088231, ; 424: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 106
	i32 2752995522, ; 425: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 311
	i32 2758225723, ; 426: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 202
	i32 2764765095, ; 427: Microsoft.Maui.dll => 0xa4caf7a7 => 203
	i32 2765824710, ; 428: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 134
	i32 2770495804, ; 429: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 283
	i32 2778768386, ; 430: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 274
	i32 2779977773, ; 431: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 262
	i32 2785988530, ; 432: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 317
	i32 2788224221, ; 433: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 240
	i32 2801831435, ; 434: Microsoft.Maui.Graphics => 0xa7008e0b => 205
	i32 2803228030, ; 435: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 160
	i32 2806116107, ; 436: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 296
	i32 2810250172, ; 437: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 227
	i32 2819470561, ; 438: System.Xml.dll => 0xa80db4e1 => 164
	i32 2821205001, ; 439: System.ServiceProcess.dll => 0xa8282c09 => 133
	i32 2821294376, ; 440: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 262
	i32 2824502124, ; 441: System.Xml.XmlDocument => 0xa85a7b6c => 162
	i32 2831556043, ; 442: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 309
	i32 2833784645, ; 443: Microsoft.AspNetCore.Metadata => 0xa8e81f45 => 181
	i32 2838993487, ; 444: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 251
	i32 2849599387, ; 445: System.Threading.Overlapped.dll => 0xa9d96f9b => 141
	i32 2853208004, ; 446: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 274
	i32 2855708567, ; 447: Xamarin.AndroidX.Transition => 0xaa36a797 => 270
	i32 2861098320, ; 448: Mono.Android.Export.dll => 0xaa88e550 => 170
	i32 2861189240, ; 449: Microsoft.Maui.Essentials => 0xaa8a4878 => 204
	i32 2870099610, ; 450: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 212
	i32 2875164099, ; 451: Jsr305Binding.dll => 0xab5f85c3 => 279
	i32 2875220617, ; 452: System.Globalization.Calendars.dll => 0xab606289 => 41
	i32 2884993177, ; 453: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 238
	i32 2887636118, ; 454: System.Net.dll => 0xac1dd496 => 82
	i32 2892341533, ; 455: Microsoft.AspNetCore.Components.Web => 0xac65a11d => 178
	i32 2899753641, ; 456: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 57
	i32 2900621748, ; 457: System.Dynamic.Runtime.dll => 0xace3f9b4 => 38
	i32 2901442782, ; 458: System.Reflection => 0xacf080de => 98
	i32 2905242038, ; 459: mscorlib.dll => 0xad2a79b6 => 167
	i32 2909740682, ; 460: System.Private.CoreLib => 0xad6f1e8a => 173
	i32 2911054922, ; 461: Microsoft.Extensions.FileProviders.Physical.dll => 0xad832c4a => 192
	i32 2916838712, ; 462: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 275
	i32 2919462931, ; 463: System.Numerics.Vectors.dll => 0xae037813 => 83
	i32 2921128767, ; 464: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 214
	i32 2936416060, ; 465: System.Resources.Reader => 0xaf06273c => 99
	i32 2940926066, ; 466: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 31
	i32 2942453041, ; 467: System.Xml.XPath.XDocument => 0xaf624531 => 160
	i32 2959614098, ; 468: System.ComponentModel.dll => 0xb0682092 => 19
	i32 2968338931, ; 469: System.Security.Principal.Windows => 0xb0ed41f3 => 128
	i32 2972252294, ; 470: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 120
	i32 2978675010, ; 471: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 234
	i32 2987532451, ; 472: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 265
	i32 2996846495, ; 473: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 247
	i32 3016983068, ; 474: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 267
	i32 3023353419, ; 475: WindowsBase.dll => 0xb434b64b => 166
	i32 3024354802, ; 476: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 242
	i32 3038032645, ; 477: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 327
	i32 3056245963, ; 478: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 264
	i32 3057625584, ; 479: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 255
	i32 3059408633, ; 480: Mono.Android.Runtime => 0xb65adef9 => 171
	i32 3059793426, ; 481: System.ComponentModel.Primitives => 0xb660be12 => 17
	i32 3075834255, ; 482: System.Threading.Tasks => 0xb755818f => 145
	i32 3077302341, ; 483: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 302
	i32 3090735792, ; 484: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 126
	i32 3099732863, ; 485: System.Security.Claims.dll => 0xb8c22b7f => 119
	i32 3103600923, ; 486: System.Formats.Asn1 => 0xb8fd311b => 39
	i32 3111772706, ; 487: System.Runtime.Serialization => 0xb979e222 => 116
	i32 3121463068, ; 488: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 48
	i32 3124832203, ; 489: System.Threading.Tasks.Extensions => 0xba4127cb => 143
	i32 3132293585, ; 490: System.Security.AccessControl => 0xbab301d1 => 118
	i32 3147165239, ; 491: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 35
	i32 3148237826, ; 492: GoogleGson.dll => 0xbba64c02 => 174
	i32 3159123045, ; 493: System.Reflection.Primitives.dll => 0xbc4c6465 => 96
	i32 3160747431, ; 494: System.IO.MemoryMappedFiles => 0xbc652da7 => 54
	i32 3178803400, ; 495: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 256
	i32 3192346100, ; 496: System.Security.SecureString => 0xbe4755f4 => 130
	i32 3193515020, ; 497: System.Web => 0xbe592c0c => 154
	i32 3204380047, ; 498: System.Data.dll => 0xbefef58f => 25
	i32 3209718065, ; 499: System.Xml.XmlDocument.dll => 0xbf506931 => 162
	i32 3211777861, ; 500: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 233
	i32 3220365878, ; 501: System.Threading => 0xbff2e236 => 149
	i32 3226221578, ; 502: System.Runtime.Handles.dll => 0xc04c3c0a => 105
	i32 3251039220, ; 503: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 90
	i32 3258312781, ; 504: Xamarin.AndroidX.CardView => 0xc235e84d => 221
	i32 3265493905, ; 505: System.Linq.Queryable.dll => 0xc2a37b91 => 61
	i32 3265893370, ; 506: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 143
	i32 3277815716, ; 507: System.Resources.Writer.dll => 0xc35f7fa4 => 101
	i32 3279906254, ; 508: Microsoft.Win32.Registry.dll => 0xc37f65ce => 6
	i32 3280506390, ; 509: System.ComponentModel.Annotations.dll => 0xc3888e16 => 14
	i32 3290767353, ; 510: System.Security.Cryptography.Encoding => 0xc4251ff9 => 123
	i32 3299363146, ; 511: System.Text.Encoding => 0xc4a8494a => 136
	i32 3303498502, ; 512: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 29
	i32 3305363605, ; 513: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 297
	i32 3316684772, ; 514: System.Net.Requests.dll => 0xc5b097e4 => 73
	i32 3317135071, ; 515: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 231
	i32 3317144872, ; 516: System.Data => 0xc5b79d28 => 25
	i32 3340431453, ; 517: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 219
	i32 3345895724, ; 518: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 260
	i32 3346324047, ; 519: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 257
	i32 3357674450, ; 520: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 314
	i32 3358260929, ; 521: System.Text.Json => 0xc82afec1 => 138
	i32 3362336904, ; 522: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 212
	i32 3362522851, ; 523: Xamarin.AndroidX.Core => 0xc86c06e3 => 228
	i32 3366347497, ; 524: Java.Interop => 0xc8a662e9 => 169
	i32 3374999561, ; 525: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 261
	i32 3381016424, ; 526: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 293
	i32 3395150330, ; 527: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 102
	i32 3403906625, ; 528: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 124
	i32 3405233483, ; 529: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 232
	i32 3406629867, ; 530: Microsoft.Extensions.FileProviders.Composite.dll => 0xcb0d0beb => 190
	i32 3421170118, ; 531: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 184
	i32 3428513518, ; 532: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 187
	i32 3429136800, ; 533: System.Xml => 0xcc6479a0 => 164
	i32 3430777524, ; 534: netstandard => 0xcc7d82b4 => 168
	i32 3441283291, ; 535: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 235
	i32 3445260447, ; 536: System.Formats.Tar => 0xcd5a809f => 40
	i32 3452344032, ; 537: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 200
	i32 3463511458, ; 538: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 301
	i32 3464190856, ; 539: Microsoft.AspNetCore.Components.Forms.dll => 0xce7b5b88 => 177
	i32 3471940407, ; 540: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 18
	i32 3476120550, ; 541: Mono.Android => 0xcf3163e6 => 172
	i32 3479583265, ; 542: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 314
	i32 3484440000, ; 543: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 313
	i32 3485117614, ; 544: System.Text.Json.dll => 0xcfbaacae => 138
	i32 3486566296, ; 545: System.Transactions => 0xcfd0c798 => 151
	i32 3493954962, ; 546: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 224
	i32 3500000672, ; 547: Microsoft.JSInterop => 0xd09dc5a0 => 199
	i32 3509114376, ; 548: System.Xml.Linq => 0xd128d608 => 156
	i32 3515174580, ; 549: System.Security.dll => 0xd1854eb4 => 131
	i32 3530912306, ; 550: System.Configuration => 0xd2757232 => 20
	i32 3539954161, ; 551: System.Net.HttpListener => 0xd2ff69f1 => 66
	i32 3560100363, ; 552: System.Threading.Timer => 0xd432d20b => 148
	i32 3570554715, ; 553: System.IO.FileSystem.AccessControl => 0xd4d2575b => 48
	i32 3580758918, ; 554: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 321
	i32 3592435036, ; 555: Microsoft.Extensions.Localization.Abstractions => 0xd620355c => 194
	i32 3597029428, ; 556: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 210
	i32 3598340787, ; 557: System.Net.WebSockets.Client => 0xd67a52b3 => 80
	i32 3608519521, ; 558: System.Linq.dll => 0xd715a361 => 62
	i32 3624195450, ; 559: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 107
	i32 3627220390, ; 560: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 259
	i32 3633644679, ; 561: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 214
	i32 3638274909, ; 562: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 50
	i32 3641597786, ; 563: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 245
	i32 3643446276, ; 564: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 318
	i32 3643854240, ; 565: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 256
	i32 3645089577, ; 566: System.ComponentModel.DataAnnotations => 0xd943a729 => 15
	i32 3657292374, ; 567: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 183
	i32 3660523487, ; 568: System.Net.NetworkInformation => 0xda2f27df => 69
	i32 3672681054, ; 569: Mono.Android.dll => 0xdae8aa5e => 172
	i32 3676670898, ; 570: Microsoft.Maui.Controls.HotReload.Forms => 0xdb258bb2 => 324
	i32 3682565725, ; 571: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 220
	i32 3684561358, ; 572: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 224
	i32 3697841164, ; 573: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 323
	i32 3700866549, ; 574: System.Net.WebProxy.dll => 0xdc96bdf5 => 79
	i32 3706696989, ; 575: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 229
	i32 3716563718, ; 576: System.Runtime.Intrinsics => 0xdd864306 => 109
	i32 3718780102, ; 577: Xamarin.AndroidX.Annotation => 0xdda814c6 => 213
	i32 3722202641, ; 578: Microsoft.Extensions.Configuration.Json.dll => 0xdddc4e11 => 186
	i32 3724971120, ; 579: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 255
	i32 3732100267, ; 580: System.Net.NameResolution => 0xde7354ab => 68
	i32 3732214720, ; 581: Microsoft.AspNetCore.Metadata.dll => 0xde7513c0 => 181
	i32 3737834244, ; 582: System.Net.Http.Json.dll => 0xdecad304 => 64
	i32 3748608112, ; 583: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 28
	i32 3751444290, ; 584: System.Xml.XPath => 0xdf9a7f42 => 161
	i32 3758424670, ; 585: Microsoft.Extensions.Configuration.FileExtensions => 0xe005025e => 185
	i32 3772663869, ; 586: MyCompany.dll => 0xe0de483d => 1
	i32 3776403777, ; 587: Microsoft.Extensions.Localization.Abstractions.dll => 0xe1175941 => 194
	i32 3786282454, ; 588: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 222
	i32 3792276235, ; 589: System.Collections.NonGeneric => 0xe2098b0b => 11
	i32 3800979733, ; 590: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 200
	i32 3802395368, ; 591: System.Collections.Specialized.dll => 0xe2a3f2e8 => 12
	i32 3819260425, ; 592: System.Net.WebProxy => 0xe3a54a09 => 79
	i32 3823082795, ; 593: System.Security.Cryptography.dll => 0xe3df9d2b => 127
	i32 3829621856, ; 594: System.Numerics.dll => 0xe4436460 => 84
	i32 3841636137, ; 595: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 188
	i32 3844307129, ; 596: System.Net.Mail.dll => 0xe52378b9 => 67
	i32 3849253459, ; 597: System.Runtime.InteropServices.dll => 0xe56ef253 => 108
	i32 3870376305, ; 598: System.Net.HttpListener.dll => 0xe6b14171 => 66
	i32 3873536506, ; 599: System.Security.Principal => 0xe6e179fa => 129
	i32 3875112723, ; 600: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 123
	i32 3885497537, ; 601: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 78
	i32 3885922214, ; 602: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 270
	i32 3888767677, ; 603: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 260
	i32 3889960447, ; 604: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 322
	i32 3896106733, ; 605: System.Collections.Concurrent.dll => 0xe839deed => 9
	i32 3896760992, ; 606: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 228
	i32 3901907137, ; 607: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 3
	i32 3920810846, ; 608: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 45
	i32 3921031405, ; 609: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 273
	i32 3928044579, ; 610: System.Xml.ReaderWriter => 0xea213423 => 157
	i32 3930554604, ; 611: System.Security.Principal.dll => 0xea4780ec => 129
	i32 3931092270, ; 612: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 258
	i32 3945713374, ; 613: System.Data.DataSetExtensions.dll => 0xeb2ecede => 24
	i32 3953953790, ; 614: System.Text.Encoding.CodePages => 0xebac8bfe => 134
	i32 3955647286, ; 615: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 216
	i32 3959773229, ; 616: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 247
	i32 3980434154, ; 617: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 317
	i32 3987592930, ; 618: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 299
	i32 4003436829, ; 619: System.Diagnostics.Process.dll => 0xee9f991d => 30
	i32 4015948917, ; 620: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 215
	i32 4023392905, ; 621: System.IO.Pipelines => 0xefd01a89 => 206
	i32 4025784931, ; 622: System.Memory => 0xeff49a63 => 63
	i32 4046471985, ; 623: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 202
	i32 4054681211, ; 624: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 91
	i32 4068434129, ; 625: System.Private.Xml.Linq.dll => 0xf27f60d1 => 88
	i32 4073602200, ; 626: System.Threading.dll => 0xf2ce3c98 => 149
	i32 4094352644, ; 627: Microsoft.Maui.Essentials.dll => 0xf40add04 => 204
	i32 4099507663, ; 628: System.Drawing.dll => 0xf45985cf => 37
	i32 4100113165, ; 629: System.Private.Uri => 0xf462c30d => 87
	i32 4101593132, ; 630: Xamarin.AndroidX.Emoji2 => 0xf479582c => 236
	i32 4102112229, ; 631: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 312
	i32 4125707920, ; 632: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 307
	i32 4126470640, ; 633: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 187
	i32 4127667938, ; 634: System.IO.FileSystem.Watcher => 0xf60736e2 => 51
	i32 4130442656, ; 635: System.AppContext => 0xf6318da0 => 7
	i32 4147896353, ; 636: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 91
	i32 4150914736, ; 637: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 319
	i32 4151237749, ; 638: System.Core => 0xf76edc75 => 22
	i32 4159265925, ; 639: System.Xml.XmlSerializer => 0xf7e95c85 => 163
	i32 4161255271, ; 640: System.Reflection.TypeExtensions => 0xf807b767 => 97
	i32 4164802419, ; 641: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 51
	i32 4181436372, ; 642: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 114
	i32 4182413190, ; 643: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 252
	i32 4182880526, ; 644: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 0xf951b10e => 325
	i32 4185676441, ; 645: System.Security => 0xf97c5a99 => 131
	i32 4196529839, ; 646: System.Net.WebClient.dll => 0xfa21f6af => 77
	i32 4213026141, ; 647: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 28
	i32 4256097574, ; 648: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 229
	i32 4258378803, ; 649: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 251
	i32 4260525087, ; 650: System.Buffers => 0xfdf2741f => 8
	i32 4271975918, ; 651: Microsoft.Maui.Controls.dll => 0xfea12dee => 201
	i32 4274976490, ; 652: System.Runtime.Numerics => 0xfecef6ea => 111
	i32 4292120959, ; 653: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 252
	i32 4294648842, ; 654: Microsoft.Extensions.FileProviders.Embedded => 0xfffb240a => 191
	i32 4294763496 ; 655: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 238
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [656 x i32] [
	i32 69, ; 0
	i32 68, ; 1
	i32 109, ; 2
	i32 248, ; 3
	i32 282, ; 4
	i32 49, ; 5
	i32 81, ; 6
	i32 146, ; 7
	i32 31, ; 8
	i32 323, ; 9
	i32 125, ; 10
	i32 205, ; 11
	i32 103, ; 12
	i32 266, ; 13
	i32 108, ; 14
	i32 266, ; 15
	i32 140, ; 16
	i32 286, ; 17
	i32 78, ; 18
	i32 125, ; 19
	i32 14, ; 20
	i32 222, ; 21
	i32 133, ; 22
	i32 268, ; 23
	i32 152, ; 24
	i32 0, ; 25
	i32 320, ; 26
	i32 321, ; 27
	i32 19, ; 28
	i32 220, ; 29
	i32 27, ; 30
	i32 242, ; 31
	i32 2, ; 32
	i32 60, ; 33
	i32 43, ; 34
	i32 92, ; 35
	i32 176, ; 36
	i32 225, ; 37
	i32 148, ; 38
	i32 244, ; 39
	i32 241, ; 40
	i32 292, ; 41
	i32 55, ; 42
	i32 70, ; 43
	i32 320, ; 44
	i32 211, ; 45
	i32 84, ; 46
	i32 305, ; 47
	i32 243, ; 48
	i32 304, ; 49
	i32 132, ; 50
	i32 56, ; 51
	i32 150, ; 52
	i32 75, ; 53
	i32 146, ; 54
	i32 63, ; 55
	i32 147, ; 56
	i32 327, ; 57
	i32 166, ; 58
	i32 316, ; 59
	i32 226, ; 60
	i32 13, ; 61
	i32 239, ; 62
	i32 126, ; 63
	i32 153, ; 64
	i32 114, ; 65
	i32 167, ; 66
	i32 165, ; 67
	i32 241, ; 68
	i32 254, ; 69
	i32 85, ; 70
	i32 303, ; 71
	i32 297, ; 72
	i32 198, ; 73
	i32 151, ; 74
	i32 286, ; 75
	i32 61, ; 76
	i32 195, ; 77
	i32 52, ; 78
	i32 104, ; 79
	i32 115, ; 80
	i32 41, ; 81
	i32 279, ; 82
	i32 277, ; 83
	i32 191, ; 84
	i32 121, ; 85
	i32 311, ; 86
	i32 53, ; 87
	i32 45, ; 88
	i32 120, ; 89
	i32 231, ; 90
	i32 309, ; 91
	i32 237, ; 92
	i32 82, ; 93
	i32 137, ; 94
	i32 273, ; 95
	i32 218, ; 96
	i32 9, ; 97
	i32 74, ; 98
	i32 291, ; 99
	i32 156, ; 100
	i32 288, ; 101
	i32 155, ; 102
	i32 93, ; 103
	i32 283, ; 104
	i32 46, ; 105
	i32 306, ; 106
	i32 294, ; 107
	i32 287, ; 108
	i32 110, ; 109
	i32 130, ; 110
	i32 26, ; 111
	i32 208, ; 112
	i32 73, ; 113
	i32 56, ; 114
	i32 47, ; 115
	i32 315, ; 116
	i32 197, ; 117
	i32 232, ; 118
	i32 180, ; 119
	i32 23, ; 120
	i32 246, ; 121
	i32 87, ; 122
	i32 44, ; 123
	i32 161, ; 124
	i32 72, ; 125
	i32 259, ; 126
	i32 4, ; 127
	i32 43, ; 128
	i32 64, ; 129
	i32 17, ; 130
	i32 54, ; 131
	i32 318, ; 132
	i32 282, ; 133
	i32 106, ; 134
	i32 287, ; 135
	i32 280, ; 136
	i32 243, ; 137
	i32 35, ; 138
	i32 159, ; 139
	i32 86, ; 140
	i32 33, ; 141
	i32 13, ; 142
	i32 52, ; 143
	i32 193, ; 144
	i32 57, ; 145
	i32 263, ; 146
	i32 37, ; 147
	i32 188, ; 148
	i32 293, ; 149
	i32 281, ; 150
	i32 216, ; 151
	i32 36, ; 152
	i32 59, ; 153
	i32 250, ; 154
	i32 174, ; 155
	i32 18, ; 156
	i32 284, ; 157
	i32 165, ; 158
	i32 185, ; 159
	i32 306, ; 160
	i32 249, ; 161
	i32 276, ; 162
	i32 312, ; 163
	i32 154, ; 164
	i32 189, ; 165
	i32 272, ; 166
	i32 257, ; 167
	i32 310, ; 168
	i32 218, ; 169
	i32 30, ; 170
	i32 53, ; 171
	i32 308, ; 172
	i32 277, ; 173
	i32 6, ; 174
	i32 292, ; 175
	i32 267, ; 176
	i32 0, ; 177
	i32 271, ; 178
	i32 223, ; 179
	i32 288, ; 180
	i32 215, ; 181
	i32 234, ; 182
	i32 86, ; 183
	i32 276, ; 184
	i32 62, ; 185
	i32 113, ; 186
	i32 58, ; 187
	i32 322, ; 188
	i32 263, ; 189
	i32 100, ; 190
	i32 20, ; 191
	i32 227, ; 192
	i32 112, ; 193
	i32 102, ; 194
	i32 103, ; 195
	i32 290, ; 196
	i32 105, ; 197
	i32 280, ; 198
	i32 72, ; 199
	i32 39, ; 200
	i32 33, ; 201
	i32 190, ; 202
	i32 104, ; 203
	i32 74, ; 204
	i32 296, ; 205
	i32 10, ; 206
	i32 124, ; 207
	i32 47, ; 208
	i32 217, ; 209
	i32 198, ; 210
	i32 10, ; 211
	i32 44, ; 212
	i32 5, ; 213
	i32 264, ; 214
	i32 300, ; 215
	i32 295, ; 216
	i32 193, ; 217
	i32 32, ; 218
	i32 139, ; 219
	i32 93, ; 220
	i32 180, ; 221
	i32 94, ; 222
	i32 315, ; 223
	i32 50, ; 224
	i32 142, ; 225
	i32 113, ; 226
	i32 141, ; 227
	i32 233, ; 228
	i32 116, ; 229
	i32 281, ; 230
	i32 158, ; 231
	i32 324, ; 232
	i32 77, ; 233
	i32 80, ; 234
	i32 253, ; 235
	i32 38, ; 236
	i32 275, ; 237
	i32 186, ; 238
	i32 237, ; 239
	i32 230, ; 240
	i32 65, ; 241
	i32 139, ; 242
	i32 16, ; 243
	i32 179, ; 244
	i32 117, ; 245
	i32 269, ; 246
	i32 278, ; 247
	i32 225, ; 248
	i32 49, ; 249
	i32 71, ; 250
	i32 81, ; 251
	i32 127, ; 252
	i32 95, ; 253
	i32 122, ; 254
	i32 285, ; 255
	i32 27, ; 256
	i32 246, ; 257
	i32 98, ; 258
	i32 29, ; 259
	i32 221, ; 260
	i32 313, ; 261
	i32 291, ; 262
	i32 150, ; 263
	i32 206, ; 264
	i32 170, ; 265
	i32 5, ; 266
	i32 99, ; 267
	i32 178, ; 268
	i32 34, ; 269
	i32 94, ; 270
	i32 268, ; 271
	i32 195, ; 272
	i32 22, ; 273
	i32 42, ; 274
	i32 171, ; 275
	i32 307, ; 276
	i32 239, ; 277
	i32 299, ; 278
	i32 253, ; 279
	i32 284, ; 280
	i32 278, ; 281
	i32 258, ; 282
	i32 3, ; 283
	i32 135, ; 284
	i32 112, ; 285
	i32 326, ; 286
	i32 196, ; 287
	i32 319, ; 288
	i32 208, ; 289
	i32 316, ; 290
	i32 59, ; 291
	i32 96, ; 292
	i32 298, ; 293
	i32 40, ; 294
	i32 219, ; 295
	i32 326, ; 296
	i32 26, ; 297
	i32 95, ; 298
	i32 90, ; 299
	i32 100, ; 300
	i32 11, ; 301
	i32 88, ; 302
	i32 101, ; 303
	i32 265, ; 304
	i32 182, ; 305
	i32 285, ; 306
	i32 210, ; 307
	i32 295, ; 308
	i32 8, ; 309
	i32 250, ; 310
	i32 290, ; 311
	i32 207, ; 312
	i32 89, ; 313
	i32 184, ; 314
	i32 245, ; 315
	i32 155, ; 316
	i32 294, ; 317
	i32 34, ; 318
	i32 192, ; 319
	i32 117, ; 320
	i32 83, ; 321
	i32 21, ; 322
	i32 12, ; 323
	i32 163, ; 324
	i32 4, ; 325
	i32 203, ; 326
	i32 302, ; 327
	i32 197, ; 328
	i32 196, ; 329
	i32 85, ; 330
	i32 289, ; 331
	i32 65, ; 332
	i32 304, ; 333
	i32 272, ; 334
	i32 144, ; 335
	i32 254, ; 336
	i32 158, ; 337
	i32 42, ; 338
	i32 118, ; 339
	i32 183, ; 340
	i32 209, ; 341
	i32 298, ; 342
	i32 261, ; 343
	i32 132, ; 344
	i32 76, ; 345
	i32 67, ; 346
	i32 308, ; 347
	i32 173, ; 348
	i32 213, ; 349
	i32 144, ; 350
	i32 107, ; 351
	i32 152, ; 352
	i32 71, ; 353
	i32 157, ; 354
	i32 182, ; 355
	i32 122, ; 356
	i32 128, ; 357
	i32 303, ; 358
	i32 153, ; 359
	i32 236, ; 360
	i32 325, ; 361
	i32 176, ; 362
	i32 142, ; 363
	i32 223, ; 364
	i32 300, ; 365
	i32 21, ; 366
	i32 15, ; 367
	i32 199, ; 368
	i32 136, ; 369
	i32 76, ; 370
	i32 60, ; 371
	i32 226, ; 372
	i32 168, ; 373
	i32 169, ; 374
	i32 201, ; 375
	i32 16, ; 376
	i32 75, ; 377
	i32 1, ; 378
	i32 7, ; 379
	i32 24, ; 380
	i32 248, ; 381
	i32 207, ; 382
	i32 175, ; 383
	i32 92, ; 384
	i32 301, ; 385
	i32 2, ; 386
	i32 137, ; 387
	i32 249, ; 388
	i32 271, ; 389
	i32 135, ; 390
	i32 70, ; 391
	i32 177, ; 392
	i32 147, ; 393
	i32 189, ; 394
	i32 310, ; 395
	i32 289, ; 396
	i32 240, ; 397
	i32 89, ; 398
	i32 97, ; 399
	i32 230, ; 400
	i32 235, ; 401
	i32 305, ; 402
	i32 32, ; 403
	i32 46, ; 404
	i32 244, ; 405
	i32 209, ; 406
	i32 110, ; 407
	i32 159, ; 408
	i32 36, ; 409
	i32 23, ; 410
	i32 115, ; 411
	i32 179, ; 412
	i32 58, ; 413
	i32 269, ; 414
	i32 145, ; 415
	i32 119, ; 416
	i32 121, ; 417
	i32 111, ; 418
	i32 211, ; 419
	i32 140, ; 420
	i32 175, ; 421
	i32 217, ; 422
	i32 55, ; 423
	i32 106, ; 424
	i32 311, ; 425
	i32 202, ; 426
	i32 203, ; 427
	i32 134, ; 428
	i32 283, ; 429
	i32 274, ; 430
	i32 262, ; 431
	i32 317, ; 432
	i32 240, ; 433
	i32 205, ; 434
	i32 160, ; 435
	i32 296, ; 436
	i32 227, ; 437
	i32 164, ; 438
	i32 133, ; 439
	i32 262, ; 440
	i32 162, ; 441
	i32 309, ; 442
	i32 181, ; 443
	i32 251, ; 444
	i32 141, ; 445
	i32 274, ; 446
	i32 270, ; 447
	i32 170, ; 448
	i32 204, ; 449
	i32 212, ; 450
	i32 279, ; 451
	i32 41, ; 452
	i32 238, ; 453
	i32 82, ; 454
	i32 178, ; 455
	i32 57, ; 456
	i32 38, ; 457
	i32 98, ; 458
	i32 167, ; 459
	i32 173, ; 460
	i32 192, ; 461
	i32 275, ; 462
	i32 83, ; 463
	i32 214, ; 464
	i32 99, ; 465
	i32 31, ; 466
	i32 160, ; 467
	i32 19, ; 468
	i32 128, ; 469
	i32 120, ; 470
	i32 234, ; 471
	i32 265, ; 472
	i32 247, ; 473
	i32 267, ; 474
	i32 166, ; 475
	i32 242, ; 476
	i32 327, ; 477
	i32 264, ; 478
	i32 255, ; 479
	i32 171, ; 480
	i32 17, ; 481
	i32 145, ; 482
	i32 302, ; 483
	i32 126, ; 484
	i32 119, ; 485
	i32 39, ; 486
	i32 116, ; 487
	i32 48, ; 488
	i32 143, ; 489
	i32 118, ; 490
	i32 35, ; 491
	i32 174, ; 492
	i32 96, ; 493
	i32 54, ; 494
	i32 256, ; 495
	i32 130, ; 496
	i32 154, ; 497
	i32 25, ; 498
	i32 162, ; 499
	i32 233, ; 500
	i32 149, ; 501
	i32 105, ; 502
	i32 90, ; 503
	i32 221, ; 504
	i32 61, ; 505
	i32 143, ; 506
	i32 101, ; 507
	i32 6, ; 508
	i32 14, ; 509
	i32 123, ; 510
	i32 136, ; 511
	i32 29, ; 512
	i32 297, ; 513
	i32 73, ; 514
	i32 231, ; 515
	i32 25, ; 516
	i32 219, ; 517
	i32 260, ; 518
	i32 257, ; 519
	i32 314, ; 520
	i32 138, ; 521
	i32 212, ; 522
	i32 228, ; 523
	i32 169, ; 524
	i32 261, ; 525
	i32 293, ; 526
	i32 102, ; 527
	i32 124, ; 528
	i32 232, ; 529
	i32 190, ; 530
	i32 184, ; 531
	i32 187, ; 532
	i32 164, ; 533
	i32 168, ; 534
	i32 235, ; 535
	i32 40, ; 536
	i32 200, ; 537
	i32 301, ; 538
	i32 177, ; 539
	i32 18, ; 540
	i32 172, ; 541
	i32 314, ; 542
	i32 313, ; 543
	i32 138, ; 544
	i32 151, ; 545
	i32 224, ; 546
	i32 199, ; 547
	i32 156, ; 548
	i32 131, ; 549
	i32 20, ; 550
	i32 66, ; 551
	i32 148, ; 552
	i32 48, ; 553
	i32 321, ; 554
	i32 194, ; 555
	i32 210, ; 556
	i32 80, ; 557
	i32 62, ; 558
	i32 107, ; 559
	i32 259, ; 560
	i32 214, ; 561
	i32 50, ; 562
	i32 245, ; 563
	i32 318, ; 564
	i32 256, ; 565
	i32 15, ; 566
	i32 183, ; 567
	i32 69, ; 568
	i32 172, ; 569
	i32 324, ; 570
	i32 220, ; 571
	i32 224, ; 572
	i32 323, ; 573
	i32 79, ; 574
	i32 229, ; 575
	i32 109, ; 576
	i32 213, ; 577
	i32 186, ; 578
	i32 255, ; 579
	i32 68, ; 580
	i32 181, ; 581
	i32 64, ; 582
	i32 28, ; 583
	i32 161, ; 584
	i32 185, ; 585
	i32 1, ; 586
	i32 194, ; 587
	i32 222, ; 588
	i32 11, ; 589
	i32 200, ; 590
	i32 12, ; 591
	i32 79, ; 592
	i32 127, ; 593
	i32 84, ; 594
	i32 188, ; 595
	i32 67, ; 596
	i32 108, ; 597
	i32 66, ; 598
	i32 129, ; 599
	i32 123, ; 600
	i32 78, ; 601
	i32 270, ; 602
	i32 260, ; 603
	i32 322, ; 604
	i32 9, ; 605
	i32 228, ; 606
	i32 3, ; 607
	i32 45, ; 608
	i32 273, ; 609
	i32 157, ; 610
	i32 129, ; 611
	i32 258, ; 612
	i32 24, ; 613
	i32 134, ; 614
	i32 216, ; 615
	i32 247, ; 616
	i32 317, ; 617
	i32 299, ; 618
	i32 30, ; 619
	i32 215, ; 620
	i32 206, ; 621
	i32 63, ; 622
	i32 202, ; 623
	i32 91, ; 624
	i32 88, ; 625
	i32 149, ; 626
	i32 204, ; 627
	i32 37, ; 628
	i32 87, ; 629
	i32 236, ; 630
	i32 312, ; 631
	i32 307, ; 632
	i32 187, ; 633
	i32 51, ; 634
	i32 7, ; 635
	i32 91, ; 636
	i32 319, ; 637
	i32 22, ; 638
	i32 163, ; 639
	i32 97, ; 640
	i32 51, ; 641
	i32 114, ; 642
	i32 252, ; 643
	i32 325, ; 644
	i32 131, ; 645
	i32 77, ; 646
	i32 28, ; 647
	i32 229, ; 648
	i32 251, ; 649
	i32 8, ; 650
	i32 201, ; 651
	i32 111, ; 652
	i32 252, ; 653
	i32 191, ; 654
	i32 238 ; 655
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ cdb777a0c306e3e0668f847433f82144d7ca745f"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
