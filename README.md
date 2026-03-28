# ASP.NET Core Web API サンプル

C#未経験から1日でCRUD + SQLite永続化 + AI連携APIを構築したサンプルです。

## 技術スタック

- ASP.NET Core Web API (.NET 10.0)
- Entity Framework Core
- SQLite
- C#

## 機能

| エンドポイント | メソッド | 説明 |
|---|---|---|
| /api/todos | GET | Todo一覧取得 |
| /api/todos | POST | Todo作成 |
| /api/todos/{id} | PUT | Todo更新 |
| /api/todos/{id} | DELETE | Todo削除 |
| /api/ai/analyze | POST | テキスト分析（AI連携） |
| /todos | GET | Todo一覧ページ（Razor Pages） |

## セットアップ

```bash
git clone https://github.com/ugu00x-cell/webapi-sample.git
cd webapi-sample
dotnet run
```

## 動作確認

```bash
# Todo作成
curl -X POST http://localhost:5050/api/todos \
  -H "Content-Type: application/json" \
  -d '{"title": "タスク名", "isDone": false}'

# Todo一覧取得
curl http://localhost:5050/api/todos

# AI分析
curl -X POST http://localhost:5050/api/ai/analyze \
  -H "Content-Type: application/json" \
  -d '{"text": "分析したいテキスト"}'
```

ブラウザで http://localhost:5050/todos を開くとTodo一覧ページが表示されます。

## Flaskとの対応関係

| Flask | ASP.NET Core |
|---|---|
| `@app.route` | `[HttpGet]` などのAttribute |
| `request.json` | `[FromBody]` |
| `return jsonify(data)` | `return Ok(data)` |
| `abort(404)` | `return NotFound()` |
| SQLAlchemy | Entity Framework Core |
| `render_template` | Razor Pages |

## 開発背景

製造業エンジニア15年（牧野フライス製作所）がPython/Flaskの経験をベースに
C#/ASP.NET CoreへのキャッチアップをClaude Codeで実施。
C言語・C++の経験があるため構文の習得は1日で完了。

## 作者

- GitHub: [@ugu00x-cell](https://github.com/ugu00x-cell)
- 専門領域: 製造業DX / Python / データ分析 / AI連携
