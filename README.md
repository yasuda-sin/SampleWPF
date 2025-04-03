# SampleWPF
a sample project of WPF model with MVVM pattern.

## Model
クラスの定義やロジック、外部DBなどへのアクセスやロギングを行う.  

## View
GUI部分.  
ユーザからの入力を受け取る.  
ViewModelに依存し, ViewModelが持つ情報をGUIに変換する.  

## ViewModel
View空の入力に応じて, Modelを操作しViewに伝える.  
ViewModelのプロパティに変更があればViewに通知する.  

## Helper
アプリケーション全体で使用されるユーティリティクラスやヘルパークラスを格納
共通処理などをまとめる際に利用する

## Services
データアクセス、外部APIとの連携など、アプリケーションのサービス層を格納
データ永続化（ファイル、データベース）に関するコードもここに含める

# Reference
[WPF 学習用ドキュメント作りました](https://kisuke0303.sakura.ne.jp/blog/?p=340)
