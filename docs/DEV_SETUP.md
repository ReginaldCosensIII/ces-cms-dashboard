# Development Setup

This document describes the expected local development setup for the CES CMS Dashboard project.

The goal is to keep local and agent-based development predictable and repeatable.

## Current development model

At the current project phase:
- local development is centered on the `ces-cms-dashboard` repository only
- adjacent CES repositories may exist locally for later reference
- the active Antigravity implementation window should remain focused on this repo only during Phase 1A

## Local repository location

Example local repo location:

`C:\Users\Regin\source\repos\ces-cms-dashboard`

Adjacent CES repos may also exist in the same parent directory, but they are not part of the current implementation scope.

## Required tooling

The project currently expects the following local tooling:

- Git
- .NET SDK 10.0.101
- a code editor or IDE capable of ASP.NET Core development
- PostgreSQL tools and access appropriate for local or dev-server database work

## SDK pinning

This repository uses `global.json` to pin the SDK version.

Current pinned SDK:
- `.NET SDK 10.0.101`

Why this matters:
- keeps builds predictable
- avoids accidental SDK drift between environments
- keeps agent-based work aligned with the intended toolchain

## Basic environment expectations

Before implementation work begins, verify:
- Git is installed and working
- the pinned .NET SDK is installed
- the repository is on the correct feature branch
- local editor or IDE can open and work with the repo
- PostgreSQL connection details are available when needed for development configuration

## Git setup expectations

Recommended development pattern:
1. start from the correct local branch
2. verify the working tree state
3. complete the approved implementation slice
4. manually verify behavior
5. stage only intended changes
6. commit with a clear message
7. push only after the work is ready for review

## Current branch convention

Preferred branch naming pattern:
- `feat/...`

Example:
- `feat/project-foundation`

## Configuration expectations

Development configuration should:
- use environment-based settings
- avoid hardcoded secrets
- avoid production assumptions
- keep local development flexible

Do not store sensitive credentials in tracked repository files.

If local-only configuration is needed, it should remain untracked and consistent with the repository `.gitignore` rules.

## Development Database and Fallback

For the current phase, the primary database target is PostgreSQL (`ces_cms_db`).

This includes:
- PostgreSQL package integration
- connection string usage pattern
- DbContext setup
- migration-ready configuration
- baseline schema workflow

**InMemory Fallback for Local Development**
Because the local laptop cannot currently connect directly to the Dev Server DB over VPN, the application uses an isolated, disposable EF Core InMemory fallback.
- `UseInMemoryDb`: true allows the UI to run locally with seeded mock data.
- To generate migrations natively for PostgreSQL, temporarily set `UseInMemoryDb`: false, run the `dotnet ef migrations add` command, but **do not run database update**.

## Running the app locally

The exact local run steps will become more concrete once the ASP.NET project scaffold exists.

At a high level, the expected process will be:
1. restore dependencies
2. build the solution
3. apply migrations or confirm database baseline
4. run the CMS Dashboard locally
5. verify core functionality manually

## Manual verification expectations

Before a branch is considered ready:
- confirm the application builds
- confirm the application launches
- confirm the intended feature slice behaves correctly
- confirm validation and error handling are reasonable for the scope
- confirm documentation matches reality

## Antigravity usage note

For the current phase:
- open only this repository in Antigravity
- do not rely on a multi-root workspace yet
- keep the implementation chat tied to this repo and branch scope

This is meant to reduce tool friction and preserve a cleaner implementation workflow during the foundation phase.

## Local-only agent files

Local project-specific agent files may exist for convenience, but they should remain untracked unless they are intended as actual project documentation.

Global reusable agent files should remain in the developer’s machine-level agent directory rather than this repo.

## Future setup expansion

This document should be expanded later when the project reaches:
- backend FAQ delivery
- public site integration
- dev server deployment validation
- production deployment preparation

For now, this file should remain focused on the foundation-phase development environment.