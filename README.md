# 时移数易
> 中国数学文化的沉浸式体验
>

<!-- 这是一张图片，ocr 内容为： -->
![](https://img.shields.io/badge/Unity-2022.3+-000000.svg?logo=unity)  
<!-- 这是一张图片，ocr 内容为： -->
![](https://img.shields.io/badge/C%23-239120.svg?logo=c-sharp)  
<!-- 这是一张图片，ocr 内容为： -->
![](https://img.shields.io/badge/License-CC0%2FCC--BY-blue.svg)

## 📖 项目简介
《时移数易》是一款以中国古代数学文化为主题的 2.5D 科普教育游戏。玩家将扮演清代著名女数学家、天文学家**王贞仪**，在书院中经历时间错乱，通过解决数学谜题恢复时间秩序，并跟随曾祖父学习古代数学知识。

游戏融合了 RPG 元素与教育科普，通过沉浸式的交互体验，让玩家在游戏中了解《周髀算经》《孙子算经》《九章算术》等古代数学经典，以及日晷、算筹、鲁班锁等传统数学工具。

<!-- 这是一张图片，ocr 内容为：数易 开始游戏 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634152832-1dd610c7-8ddb-4f81-9fb6-c0741ea7706a.png)

<!-- 这是一张图片，ocr 内容为：音乐 锦青 拾木计数 寻牌温故 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634134294-04bb92be-add4-490e-b1b4-e6aaa0a5a1e6.png)

## 🛠️ 技术栈
### 核心工具
+ **Unity3D** - 游戏引擎（2022.3+）
+ **C#** - 主要编程语言
+ **Blender** - 3D 建模与场景搭建
+ **Procreate/Photoshop** - 2D 绘图与 UI 设计
+ **Premiere** - 视频制作

### 渲染管线
+ **URP (Universal Render Pipeline)** - 通用渲染管线
+ **Global Volume** - 后期处理（Bloom、色彩调整、暗角效果）

## 🎮 核心功能
### 1. 日影定时 (Sundial Time Fixing)
**玩法机制**：通过旋转日晷的内、中、外三层轮盘，使影子重合为一条线来修复时间。

**涉及知识**：

+ 《周髀算经》中的日晷计时原理
+ 古代天文观测与时间测量

<!-- 这是一张图片，ocr 内容为：DEBEN 规则 音乐 属 初是为正 西 中转 初正 房午 新江 正 正初 正初 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634187344-71d097ae-a02d-4db7-a881-65414bb863ee.png)

---

### 2. 拾木计数 (Stick Counting)
**玩法机制**：

+ 在庭院中使用 **WASD** 移动收集树枝
+ 集齐树枝后进行算筹计算
+ 在限定次数内点击算筹按钮，凑出目标数值

**涉及知识**：

+ 《孙子算经》中的算筹记数法
+ 古代计数系统与运算方法

<!-- 这是一张图片，ocr 内容为：锦囊 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634283991-eca817da-a03e-4cf1-b7a2-faf1ab2e1a01.png)

<!-- 这是一张图片，ocr 内容为：音乐 规则 您已输入的总值为D 您能输入的次数为! 您的目标值为十 再来一次 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634292574-697f2bb0-4691-4028-814e-8589f0f93e81.png)

---

### 3. 巧锁智拼 (Luban Lock Puzzle)
**玩法机制**：

+ 3D 拼图解谜
+ 拖拽、旋转组件（"好汉"、"三通"、"鲁班球"等结构）
+ 正确拼装后显示绿色提示，错误则自动返回

**涉及知识**：

+ 传统榫卯结构
+ 空间几何与逻辑推理

<!-- 这是一张图片，ocr 内容为：规则 音乐 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634308925-bdab7c51-e1fb-4866-88db-6c5d6cc38828.png)

---

### 4. 寻牌温故 (Card Memory Game)
**玩法机制**：

+ 记忆翻牌游戏
+ 匹配相同的古代数学家或著作卡牌
+ 考验记忆与知识储备

**涉及知识**：

+ 祖冲之、杨辉等古代数学家
+ 《九章算术》等数学经典

<!-- 这是一张图片，ocr 内容为：时数易 时数易 时数易 音乐 规则 锦囊 时数易 时数易 时数易 时数易 时数易 时数易 时数易 51 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634327616-7618aaa6-96f7-4b37-9389-551f0209911b.png)

## ⚡ 技术亮点
### 2.5D 视效实现 (FaceCamera)
游戏采用独特的 2.5D 视觉风格，结合三维场景与二维水墨风人物素材。

**实现原理**：

+ 使用 `FaceCamera` 脚本，使场景中的 2D 物体（人物、树木等）始终朝向摄像机
+ 通过动态旋转实现伪 3D 效果，营造沉浸式视觉体验
+ 配合 Global Volume 的后期处理（Bloom、色彩调整、暗角），增强画面表现力

```csharp
// FaceCamera 核心逻辑示例
void Update()
{
    transform.LookAt(Camera.main.transform);
    // 限制旋转轴，避免翻转
}
```

---

### 异步场景加载
**实现方式**：

+ 使用 `SceneManager.LoadSceneAsync` 实现异步加载
+ 避免游戏卡顿，提升用户体验

**技术细节**：

+ 协程管理加载流程
+ 加载完成后平滑过渡到新场景

---

### 背包系统 (Inventory System)
**核心功能**：

+ 动态生成物品槽位（使用 `Instantiate`）
+ 物品从场景拾取后自动消失
+ 数据同步到 UI 背包界面
+ 支持物品管理与查看

**实现逻辑**：

+ 使用 `OnTriggerEnter` 检测物品碰撞
+ 物品数据存储在单例管理器
+ UI 动态更新，实时反映背包状态

<!-- 这是一张图片，ocr 内容为：音乐 锦囊 在庭院里捡到的木棍 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634431074-5bea6ecb-38ec-4e28-9a18-09d0a46ad16a.png)

---

### 对话系统 (Dialogue System)
**功能特性**：

+ 从 `.txt` 文件读取对话内容（换行符分隔）
+ 打字机效果：使用 `IEnumerator` 协程逐字输出
+ 角色头像切换
+ 支持多角色对话与剧情推进

**实现细节**：

+ 文件解析与内容管理
+ 协程控制文字显示速度
+ UI 动画与交互反馈

```csharp
// 对话系统核心逻辑示例
IEnumerator TypeText(string text)
{
    foreach (char letter in text)
    {
        dialogueText.text += letter;
        yield return new WaitForSeconds(typingSpeed);
    }
}
```

<!-- 这是一张图片，ocr 内容为：10 5 10 10 5 161520156 贞仪 啊,下面的数是上面两个数的和! -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768634450869-ac581d17-f6d7-46c9-8b8a-7de2d2ec666c.png)

---

### 其他技术特性
+ **移动控制**：`Input.GetAxis` + `Rigidbody` 实现 WASD 人物移动
+ **相机跟随**：限制相机仅在 Y 轴跟随旋转，避免上下晃动
+ **地图与进度管理**：`MapManager` 单例模式管理全局状态，`PlayerPrefs` 本地存储关卡解锁状态
+ **物品交互**：利用 `OnTriggerEnter` 检测碰撞，实现拾取与剧情触发

## 🚀 安装与运行
### 环境要求
+ **Unity 版本**：2022.3 LTS 或更高版本
+ **操作系统**：Windows 10/11, macOS, Linux
+ **最低配置**：
    - CPU: Intel Core i5 或同等性能
    - 内存: 4GB RAM
    - 显卡: 支持 DirectX 11
    - 存储空间: 2GB 可用空间

### 运行步骤
1. **克隆项目**

```bash
git clone https://github.com/Erd-omg/ShiYiShuYi
cd SYSY
```

2. **打开项目**
    - 启动 Unity Hub
    - 点击 "Add" 或 "Open"，选择项目根目录
    - 确保 Unity 版本为 2022.3 LTS 或更高
3. **导入依赖**
    - Unity 会自动导入项目依赖
    - 等待资源导入完成（首次打开可能需要较长时间）
4. **运行游戏**
    - 在 Unity Editor 中打开主场景
    - 点击 Play 按钮开始游戏
    - 或通过 `File > Build Settings` 构建可执行文件

### 构建发布
1. 打开 `File > Build Settings`
2. 选择目标平台（Windows、macOS、Linux 等）
3. 点击 "Build" 生成可执行文件

> **注意**：首次构建可能需要下载平台相关模块，请确保网络连接正常。
>

## 📁 项目结构
```plain
SYSY/
├── Assets/
│   ├── Scenes/          # 游戏场景
│   ├── Scripts/         # C# 脚本
│   ├── Prefabs/         # 预制体
│   ├── Materials/       # 材质
│   ├── Textures/        # 贴图
│   ├── Audio/           # 音效与音乐
│   └── UI/              # UI 资源
├── ProjectSettings/     # Unity 项目设置
├── Packages/            # 包管理
└── README.md           # 项目说明文档
```

## 👥 团队分工
+ **Erd** - 组长
    - 鲁班锁关卡开发
    - 对话系统实现
    - 背包系统改进
+ **Ming** - Unity 开发
    - 庭院寻宝/算筹/日晷关卡
    - 背包系统
    - 系统维护
+ **Evan Lu** - Unity 开发
    - 场景搭建
    - 动画制作
    - 答题/地图系统
    - 翻牌游戏

## 🙏 致谢
### 素材来源
+ **卡牌预制体**：部分参考开源教程
+ **背景音乐**：来自 CC0/CC-BY 协议素材（[耳聆网](https://www.ear0.com/)）

### 参考资料
+ 《周髀算经》- 中国古代数学经典
+ 《孙子算经》- 算筹记数法
+ 《九章算术》- 古代数学著作
+ 王贞仪 - 清代女数学家、天文学家

## 📄 许可证
本项目部分素材遵循 CC0/CC-BY 协议，具体请参考各素材来源说明。

## 📧 联系方式
如有问题或建议，欢迎通过以下方式联系：

+ 项目仓库 Issues

---

## ⚠️ 敏感信息说明
本项目已对以下敏感信息进行脱敏处理：
- **PS4 Passcode**: 已用星号（*）替换
- **用户路径信息**: 通过 `.gitignore` 排除 `Library/`、`Logs/` 等包含本地路径的目录

如需配置项目，请参考 Unity 官方文档或联系项目维护者。

---

**《时移数易》** - 让数学文化在游戏中传承 ✨

