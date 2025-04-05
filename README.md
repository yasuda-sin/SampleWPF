# SampleWPF
a sample project of WPF model with MVVM pattern.

## Models
データ構造の定義  
外部APIとJSONなどの対応の定義  
データの属性などの定義  
内部的なエラー処理を担う.  

## Views
GUI部分.  
ユーザからの入力を受け取る.  
ViewModelに依存し, ViewModelが持つ情報をGUIに変換する.  

## ViewModels
View空の入力に応じて, Modelを操作しViewに伝える.  
ViewModelのプロパティに変更があればViewに通知する.  

## Helpers
アプリケーション全体で使用されるユーティリティクラスやヘルパークラスを格納.
共通処理などをまとめる際に利用する.

## Services
DBの読み書きの永続処理, 外部APIとの連携やファイル操作などの役割を担う.  


# Reference
[WPF 学習用ドキュメント作りました](https://kisuke0303.sakura.ne.jp/blog/?p=340)
