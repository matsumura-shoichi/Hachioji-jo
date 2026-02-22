# 🏯 八王子城バーチャル案内 (WebGL)
# Hachioji Castle 3D Exploration 

本プロジェクトは、八王子城跡を3D空間上で自由に探索できるアプリケーションです。
プレイヤー（ロボット）を操作し、史跡内の各地点へ移動しながら地形や遺構の理解を深めることを目的としています。

WEBGL ビルドに対応しており、ブラウザ上での操作が可能です。

---

## 🎮 主な機能

* 三人称視点によるフィールド探索
* 地点選択による瞬間移動（テレポート）
* テレポート時の演出

  * 上空からの落下モーション
  * 落下SE
  * 着地SE
  * Cinemachine Noise を利用した着地時カメラシェイク
* 各地点ごとの解説表示（※初回移動時のみ）
* WEBGL環境における360度カメラ操作対応
* マウスカーソルのロック／解除制御

---

## 🧩 使用スクリプト一覧

| Script名                | 役割                            |
| ---------------------- | ----------------------------- |
| TeleportManager.cs     | 地点移動処理（落下演出付きテレポート）           |
| CinemachineShake.cs    | Cinemachine Noise を用いたカメラ揺れ処理 |
| LocationInfoManager.cs | 地点説明UIおよび音声再生                 |
| WebGLCameraControl.cs  | WEBGL用カメラ操作・ズーム処理             |
| QuitGame.cs            | 終了処理                          |

---

## 🎥 カメラ揺れの実装について

着地時のカメラ揺れは
Unity Technologies
が提供する
Cinemachine
の **Basic Multi Channel Perlin（Noise）** を利用して実装しています。

スクリプトから振幅（Amplitude Gain）および周波数（Frequency Gain）を一時的に変更することで、
着地時のみカメラシェイクを発生させています。

## 🔧 スクリプト設定ガイド（Inspector 設定例付き）

本プロジェクトでは、各機能を以下のスクリプトによって管理しています。
Unity Editor 上での Inspector 設定と併せて解説します。

---

### 🎬 OpeningZoom.cs

ゲーム開始時に、上空からプレイヤーへズームインするオープニング演出を行います。

#### ▼ アタッチ先

```
OpeningManager（Empty GameObject）
```
![OpeningZoom Inspector](Doc/Images/CameraZoom(CameraManager).jpg)

#### ▼ Inspector 設定例

| 項目             | 設定内容                                         |
| -------------- | -------------------------------------------- |
| Opening Cam    | OpeningCam（CinemachineVirtualCamera）         |
| Player Cam     | PlayerFollowCamera（CinemachineVirtualCamera） |
| Start Distance | 100                                          |
| End Distance   | 5                                            |
| Duration       | 5                                            |

※ OpeningCam の Priority を PlayerCam より高く設定してください。

---

### 📍 TeleporterManager.cs

地点選択時のテレポート処理を管理します。
落下モーション、SE再生、着地時のカメラ揺れもここで制御します。

#### ▼ アタッチ先

```
GameManager（Empty GameObject）
```

#### ▼ Inspector 設定例

| 項目           | 設定内容                           |
| ------------ | ------------------------------ |
| Player       | PlayerArmature                 |
| Locations    | 各テレポート地点Transform              |
| Info Manager | InfoPanel（LocationInfoManager） |
| Fall SE      | 落下音AudioClip                   |
| Land SE      | 着地音AudioClip                   |
| Camera Shake | PlayerFollowCamera             |

---

### 🌍 LocationInfoManager.cs

地点移動時に、説明画像や音声ガイドを表示・再生します。
※ 同一地点へ2回目以降の移動時には表示されません。

#### ▼ アタッチ先

```
InfoPanel（UI Panel）
```

#### ▼ Inspector 設定例

| 項目           | 設定内容                       |
| ------------ | -------------------------- |
| Info Image   | DescriptionImage（UI Image） |
| Audio Source | InfoPanel                  |
| Panel        | InfoPanel                  |

---

### 📡 CinemachineShake.cs

Cinemachine の
Basic Multi Channel Perlin（Noise）を利用し、着地時のカメラ揺れを実装します。

#### ▼ アタッチ先

```
PlayerFollowCamera（CinemachineVirtualCamera）
```

#### ▼ 必須設定

CinemachineVirtualCamera の

```
Noise → Basic Multi Channel Perlin
```

を選択してください。

---

### 🖱️ WebGLCameraControl.cs

WEBGL ビルド時のマウスロック制御およびズーム操作を管理します。

#### ▼ アタッチ先

```
MainCamera
```

| 操作      | 内容               |
| ------- | ---------------- |
| 右クリック   | カメラ操作開始（カーソルロック） |
| ESC     | カーソル解除           |
| マウスホイール | ズーム              |

---

### 🚪 QuitGame.cs

ゲーム終了用UIパネルを制御します。

#### ▼ アタッチ先

```
GameManager
```

---

### 🧭 SceneLoader.cs

タイトル画面からゲームシーンへの遷移を行います。

---

### 📌 TerrainPoint.cs

テレポート地点の座標管理用スクリプトです。

---

### 📖 ReadmeIntroPlayer.cs

ゲーム開始時のイントロ表示制御を行います。

---

### 🗂️ LocationData.cs

各地点の説明画像・音声データを格納するクラスです。
---

## ⌨️ 操作方法

| 操作           | 内容            |
| ------------ | ------------- |
| 矢印キー         | 移動            |
| Shift + 矢印キー | 走行            |
| マウス右クリック     | 視点操作（カーソルロック） |
| マウスホイール      | ズーム           |
| ESCキー        | カーソル解除        |

---

## 📚 使用データ・出典

### ■ 地形データ

東京都デジタルサービス局 デジタルサービス推進部
デジタルサービス推進課 作成
**東京都デジタルツイン実現プロジェクト 多摩地域点群データ**

グリッドデータ（0.25m）を使用
https://www.geospatial.jp/ckan/dataset/tokyopc-tama-202

---

### ■ 地図データ

八王子城公式ガイドHP編集部（風魔 Project）作成
**赤色立体縄張り図**
https://tensho18.jp/k_geospm.html

---

### ■ 解説資料

八王子市教育委員会 生涯学習スポーツ部 文化財課 作成
**八王子城跡散策マップ**
https://www.city.hachioji.tokyo.jp/kurashi/kyoiku/005/bunkazaikanrenshisetsu/p005201.html

---

## ⚠️ ライセンス・注意事項

本プロジェクトは教育・研究目的で作成されています。
使用している各種データの著作権は、それぞれの作成機関に帰属します。

二次利用の際は、必ず各出典元の利用規約をご確認ください。

---

## 🛠️ 開発環境

* Unity（Third Person Starter Assets 使用）
* Cinemachine
* Windows / WEBGL
