# Maxio Advanced Billing SDK Plugin

A plugin whose skills teach a coding agent to install and use the APIMatic-generated **Maxio Advanced Billing SDK**, in C#/.NET. Every SDK fact the skills state is grounded in the SDK's own source and generated documentation, not in what a model remembers about this API.

## What's inside

One skill set per language. The entry point is that language's getting-started skill, which carries what is specific to this SDK; the rest are API-agnostic and describe how to use any SDK the same generator produces.

| Language | Skill prefix | Skills |
| --- | --- | --- |
| C#/.NET | `dotnet-` | `dotnet-authentication`, `dotnet-calling-endpoints`, `dotnet-client-initialization`, `dotnet-configuration-resilience`, `dotnet-error-handling`, `dotnet-getting-started`, `dotnet-integrate-maxio-advanced-billing`, `dotnet-models`, `dotnet-testing` |

## Agents

| Agent | What it does |
| --- | --- |
| `dotnet-maxio-advanced-billing-planner` | Writes `maxio-advanced-billing-plan.md` — the plan and its map-grounded contract sheet — before any code, and revises it in place when the main agent sends a clarification. It writes no project code. `dotnet-integrate-maxio-advanced-billing` tells the main agent when to start it. |

The same agent ships in three formats, with one body: `agents/` (Claude Code, Cursor, opencode), `codex/agents/` (Codex) and `agents-vscode/` (VS Code).

## Install

This plugin was **generated for you, not published to a shared plugin marketplace** — so you install it from a local path rather than by name. Point your coding agent at the directory holding this plugin, then install it:

```
/plugin marketplace add <path to the directory containing this plugin>
/plugin install maxio-advanced-billing
```

In Codex the same two steps are CLI commands:

```
codex plugin marketplace add <path to the directory containing this plugin>
codex plugin add maxio-advanced-billing@<marketplace>
```

`<marketplace>` is the name the directory you just added registers under — `codex plugin marketplace list` prints it.

Once your team publishes it to a plugin marketplace, add that marketplace in place of the local path and install `maxio-advanced-billing` from it by name.

Then ask a usage question (e.g. *"how do I authenticate this SDK with an API key?"*) to trigger the relevant skill.

