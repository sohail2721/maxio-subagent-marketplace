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
| `maxio-advanced-billing-planner` | Writes `maxio-advanced-billing-plan.md`, the plan and its contract sheet, before any project file changes, and revises it in place. `dotnet-integrate-maxio-advanced-billing` starts it first, on the main agent's own model. |
| `maxio-advanced-billing-implementer` | Codex only. Implements the integration from the finished plan: loads the skills the contract sheet names, writes the code, builds, runs the tests and fills the sheet's `TBD` cells, then fixes what the main agent sends back. `dotnet-integrate-maxio-advanced-billing` starts it after the plan, on the main agent's own model. |

How each host gets them:

- **Claude Code** loads the planner from `agents/` with the plugin and lists it as `maxio-advanced-billing:maxio-advanced-billing-planner`. Its file sets `model: inherit`, so it runs on the main agent's model. The implementer has no `agents/` file, so the main agent implements from the plan itself.
- **Codex** does not load `agents/*.md`, so both ship as Codex agent roles, `codex/agents/maxio-advanced-billing-planner.toml` and `codex/agents/maxio-advanced-billing-implementer.toml`. Copy them into a Codex config `agents/` folder (`~/.codex/agents/` or the project's `.codex/agents/`); the main agent then starts each by name.
- **With no subagents at all** (turned off, or a host without them), the main agent says so in one line, writes the plan itself before any code and implements it.

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

