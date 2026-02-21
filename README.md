# 🏯 八王子城バーチャル案内 (WebGL)

八王子城跡を3D空間で巡ることができるバーチャルツアーです。
ブラウザ上で動作するため、インストール不要で体験できます。

---

## 🎮 Play Online

👉 https://matsumura-shoichi.github.io/Hachioji-jo/

---

## 📌 概要

本プロジェクトは Unity WebGL を用いて制作された
**史跡探索型3Dコンテンツ** です。

プレイヤーはロボットを操作し、
各地点へ瞬間移動（テレポート）しながら
その場所に関する説明画像や音声を閲覧できます。

---

## 🕹 操作方法

| 操作         | 内容          |
| ---------- | ----------- |
| W A S D    | 移動          |
| Shift + 移動 | 走る          |
| マウス右クリック   | カメラ操作（360°） |
| マウスホイール    | ズーム         |
| ESC        | カーソル表示      |
| ドロップダウン    | 地点移動        |

---

## ✨ 主な機能

* 地点テレポート機能
* 地点ごとの解説画像表示
* ナレーション音声再生
* 初回訪問時のみ解説表示
* 落下登場演出
* 着地SE
* カメラシェイク
* オープニング演出

---

## 🧩 使用スクリプト一覧

---

### 📍 TeleportManager.cs

**役割：**
地点移動処理を管理します。

* プレイヤーを指定地点へ移動
* 落下演出の再生
* 着地SEの再生
* カメラシェイク
* 地点情報の表示指示

**使い方：**

1. Player を指定
2. LocationData を登録
3. AudioSource を設定
4. CameraShake を指定

---

### 📍 LocationData.cs

**役割：**
各地点の情報をまとめた ScriptableObject

* 移動先座標
* 解説画像
* ナレーション音声
* 初回訪問フラグ

**使い方：**

Create → Location Data
から地点データを作成し、TeleportManager に登録します。

---

### 📍 LocationInfoManager.cs

**役割：**
地点到達時の情報表示を管理

* 解説画像表示
* ナレーション再生
* 再生終了後の非表示

---

### 📍 CameraShake.cs

**役割：**
着地時のカメラ揺れ演出

MainCamera にアタッチします。

---

### 📍 ReadmeIntroPlayer.cs

**役割：**
ReadMe画面 → イントロ演出 → ゲーム画面
の遷移を制御します。

---

## 🌐 ビルド環境

* Unity 2022 LTS
* WebGL

---

## 📜 ライセンス

This project is for educational and demonstration purposes.

---

## 🚀 今後の予定

* マルチプレイヤー対応（Photon予定）
* オンライン共同探索機能
* ボイスチャット機能
