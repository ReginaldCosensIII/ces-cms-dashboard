# CES CMS Dashboard

Internal ASP.NET CMS dashboard for CES website content management.

This repository is intentionally named broadly because the dashboard may expand in the future to support additional managed content. However, the current approved implementation scope remains tightly limited to the FAQ and Tech Tip CMS foundation and related development work for Phase 3 and Phase 4.

## Current phase

The project has completed Phase 3 (UI Component Integration) and is transitioning into Phase 4 (Database Schema & API Foundation).

Phase 3 and 4 are focused on:
- stabilizing the Razor Pages CMS UI with a premium, brand-compliant aesthetic
- securing content input via a locked-down Quill.js rich text editor
- implementing Entity Framework Core models for `Faq` and `TechTip` entities
- establishing the PostgreSQL database schema and initial migrations
- scaffolding the internal CRUD workflows for content managers

## Current scope

The current approved scope covers the content management foundation for FAQs and Tech Tips for the CES website, utilizing a decoupled "headless" architecture.

In scope for the current phase:
- finalized CMS Dashboard UI (Bootstrap 5, custom brand tokens, SPA modal interactions)
- PostgreSQL integration and EF Core code-first migrations
- FAQ and Tech Tip data models and database baseline
- internal FAQ/Tech Tip CRUD workflow via Razor Pages
- project workflow and documentation standardization

Out of scope for the current phase:
- frontend JavaScript `fetch` integration on the static public site
- caching layer and cache invalidation strategies
- broader CMS content types beyond FAQs and Tech Tips
- public site migration into ASP.NET (public site remains static HTML/JS)
- production deployment implementation
- unnecessary redesigns or speculative expansion

## Repository purpose

This repository contains the standalone internal CMS Dashboard application. It acts as the "headless" content manager and API provider that will eventually serve data to the static CES website.

## Related repositories

This project exists alongside other CES repositories that are not part of the current implementation scope.

Related repos:
- `ces-website` (The static HTML/JS public frontend)
- `ces-backend-service` (Legacy/existing backend services)

Those repositories may be referenced later for integration work, but they should not be modified as part of this specific CMS Dashboard foundation branch unless explicitly approved in a later phase.

## Project structure

Planned high-level structure:

- `.github/`
  - pull request templates and repository workflow support
- `docs/`
  - project architecture, setup, database, and implementation planning documentation
- `src/`
  - application source code for the CMS Dashboard and Data API
- `wwwroot/`
  - local UI assets, custom CSS design tokens, and restricted JS bundles
- `global.json`
  - SDK pinning for predictable local and agent-based development
- `WORKFLOW.md`
  - standardized AI-assisted development workflow for this project

## Technical baseline

Planned technical stack for the current phase:
- ASP.NET Core on .NET 8/10
- Razor Pages for the internal CMS Dashboard UI
- Vanilla JS and Bootstrap 5 for UI state management (no heavy frontend frameworks)
- Secured Quill.js for rich text content generation
- PostgreSQL for data storage
- Entity Framework Core for data access and migrations
- IIS-compatible hosting model configured as a sub-application
- **CES SEO Copilot** — AI-powered chat assistant integrated directly into the Dashboard via the official OpenAI .NET SDK (`gpt-4o-mini`), a secure Razor Pages AJAX handler, and a vanilla JS `fetch` pipeline with Antiforgery token support

## Development expectations

This repository is being developed using a structured AI-assisted workflow.

Key expectations:
- all implementation work is completed on isolated feature branches (`feat/*`)
- implementation remains tightly scoped to the approved task slice
- meaningful changes are manually reviewed and verified before commit/push
- documentation is updated alongside implementation work
- the project hub controls scope, planning, and review
- the implementation agent performs execution work inside the approved branch utilizing local `/agent-skills`

## Local setup overview

At a high level, local development should follow this pattern:
1. clone the repo locally
2. ensure the pinned .NET SDK version is installed
3. launch the CMS Dashboard locally
4. manually verify functionality before staging and committing changes

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

This repository's UI foundation is complete and locked. The CES SEO Copilot AI integration (`feat/copilot-initial-setup`) has been implemented and is pending merge. The current focus is establishing the C# Entity Framework architecture to support database persistence — including future Copilot chat history logging — before advancing to public API delivery.