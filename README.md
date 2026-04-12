# CES CMS Dashboard

Internal ASP.NET CMS dashboard for CES website content management.

This repository is intentionally named broadly because the dashboard may expand in the future to support additional managed content. However, the current approved implementation scope remains tightly limited to the FAQ CMS foundation and related development work for Phase 1.

## Current phase

The project is currently in Phase 1A.

Phase 1A is focused on foundational setup only:
- solution and project structure
- standalone ASP.NET CMS Dashboard scaffold
- PostgreSQL development configuration foundation
- FAQ entity and model foundation
- baseline schema and migration setup
- minimal FAQ CRUD skeleton
- documentation and workflow baseline

## Current scope

The current approved scope is limited to FAQ content management foundation for the CES website.

In scope for the current phase:
- standalone CMS Dashboard application foundation
- PostgreSQL integration foundation
- FAQ data model and database baseline
- minimal internal FAQ CRUD workflow
- project workflow and documentation standardization

Out of scope for the current phase:
- public Contact Us page FAQ integration
- backend FAQ delivery/API work
- caching and cache invalidation
- broader CMS content types
- blog publishing
- public site migration into ASP.NET
- production deployment implementation
- unnecessary redesigns or speculative expansion

## Repository purpose

This repository contains the standalone internal CMS Dashboard application that will eventually be used to manage website content for CES.

For the current phase, this repo should remain focused on FAQ management foundation only.

## Related repositories

This project exists alongside other CES repositories that are not part of the current implementation scope for Phase 1A.

Related repos:
- `ces-website`
- `ces-backend-service`

Those repositories may be referenced later for integration work, but they should not be modified as part of this initial foundation branch unless explicitly approved in a later phase.

## Project structure

Planned high-level structure:

- `.github/`
  - pull request templates and repository workflow support
- `docs/`
  - project architecture, setup, database, and implementation planning documentation
- `src/`
  - application source code for the CMS Dashboard
- `global.json`
  - SDK pinning for predictable local and agent-based development
- `WORKFLOW.md`
  - standardized AI-assisted development workflow for this project

## Technical baseline

Planned technical stack for the current phase:
- ASP.NET Core on .NET 10
- Razor Pages for the internal CMS Dashboard
- PostgreSQL for data storage
- Entity Framework Core for data access and migrations
- IIS-compatible hosting model for future deployment targets

## Development expectations

This repository is being developed using a structured AI-assisted workflow.

Key expectations:
- all implementation work is completed on isolated feature branches
- implementation remains tightly scoped to the approved task slice
- meaningful changes are manually reviewed and verified before commit/push
- documentation is updated alongside implementation work
- the project hub controls scope, planning, and review
- the implementation agent performs execution work inside the approved branch

## Local setup overview

At a high level, local development should follow this pattern:
1. clone the repo locally
2. ensure the pinned .NET SDK version is installed
3. configure local development settings and database connection values
4. run migrations or initialize the database baseline
5. launch the CMS Dashboard locally
6. manually verify functionality before staging and committing changes

Detailed local environment guidance belongs in:
- `docs/DEV_SETUP.md`

## Documentation index

Core documentation files:
- `WORKFLOW.md`
- `docs/ARCHITECTURE.md`
- `docs/DATABASE.md`
- `docs/DEV_SETUP.md`
- `docs/IMPLEMENTATION_PLAN.md`

## Status note

This repository has been initialized and pinned to .NET 10. The current focus is to establish a clean, professional project foundation before any later-phase public integration work begins.