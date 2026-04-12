# Implementation Plan

This document tracks the planned implementation sequence for the CES CMS Dashboard project.

The repository name is intentionally broad for future growth, but the current approved implementation remains tightly limited to FAQ CMS foundation and later FAQ-related integration work.

## Project objective

Build a basic internal CMS workflow for CES FAQ content.

The FAQ content will be:
- stored in PostgreSQL
- managed through a standalone internal ASP.NET CMS Dashboard
- delivered later through the existing backend service layer
- displayed later on the public CES Contact Us page

The public CES website remains in its current static IIS deployment during this phase of the project.

## Approved estimate

Approved implementation estimate:
- 27 hours

## Phase breakdown

## Phase 0. Project planning and control
Purpose:
- define scope
- lock architecture direction
- establish execution workflow
- prepare the project foundation for implementation

Status:
- in progress / partially complete

Includes:
- project hub setup
- architecture coordination
- repo initialization
- workflow baseline
- initial documentation baseline

## Phase 1A. Project foundation and FAQ CMS bootstrap
Purpose:
Create the standalone CMS Dashboard foundation needed for future FAQ management and later integration work.

In scope:
- repository and documentation refinement
- .NET 10 solution and project scaffold
- standalone ASP.NET CMS Dashboard scaffold
- PostgreSQL development configuration foundation
- FAQ entity and model foundation
- baseline schema and migration setup
- minimal FAQ CRUD skeleton
- professionalized workflow and documentation files
- project-specific PR template refinement

Out of scope:
- backend FAQ delivery
- public Contact Us page integration
- cache implementation
- cache invalidation
- CORS
- broader CMS support
- blog features
- production deployment work

Definition of done:
- clean .NET 10 solution exists
- CMS Dashboard scaffold exists
- PostgreSQL foundation exists
- FAQ model exists
- migration baseline exists
- minimal FAQ CRUD skeleton exists
- documentation and workflow files are upgraded and accurate
- no later-phase features were added

## Phase 1B. Backend FAQ delivery foundation
Purpose:
Extend the existing CES backend service to support public FAQ retrieval.

Planned scope:
- FAQ retrieval service layer
- public-safe FAQ endpoint
- read path from PostgreSQL
- backend-side FAQ caching
- cache invalidation or refresh mechanism triggered by CMS changes

Not included in Phase 1A.

## Phase 1C. Public Contact Us page FAQ integration
Purpose:
Connect the static CES Contact Us page to the backend FAQ delivery path.

Planned scope:
- frontend fetch logic
- dynamic FAQ rendering
- graceful failure handling
- design-preserving integration into the existing page

Not included in Phase 1A.

## Phase 1D. Dev server deployment and end-to-end testing
Purpose:
Validate the complete FAQ management and display workflow in the CES development environment.

Planned scope:
- deploy CMS Dashboard to CES Dev Server
- validate PostgreSQL connectivity in dev/test
- validate backend delivery
- validate public FAQ rendering
- validate update propagation after FAQ changes

Not included in Phase 1A.

## Phase 1E. Production readiness and approval preparation
Purpose:
Prepare the system for production deployment after successful dev/test validation and approval.

Planned scope:
- deployment notes
- configuration checklist
- operational notes
- known limitations
- production deployment preparation

Not included in Phase 1A.

## Current implementation priority

Current priority:
- complete Phase 1A cleanly and narrowly

The project should not move into backend or public integration work until the foundation branch is reviewed and accepted.

## Scope guardrails

The following items are explicitly considered scope creep for the current phase unless separately approved:
- additional content types
- generalized CMS abstractions for future use
- advanced admin/auth workflows
- rich text editing systems
- media management
- blog publishing support
- public site migration
- unrelated refactors in adjacent repositories

## Working method

This project uses a structured AI-assisted workflow.

High-level execution pattern:
1. define task in project hub
2. review architecture and dependencies
3. generate implementation prompt
4. implement on isolated feature branch
5. manually verify
6. update documentation
7. review summary against done criteria
8. merge only after approval

## Documentation expectation

As each phase progresses, the following documentation should remain current:
- `README.md`
- `WORKFLOW.md`
- `docs/ARCHITECTURE.md`
- `docs/DATABASE.md`
- `docs/DEV_SETUP.md`
- `docs/IMPLEMENTATION_PLAN.md`

## Current note

Even though this repository may support broader CMS functionality later, the implementation should remain tightly controlled so that the project reaches a stable FAQ management baseline first.

A narrow, complete Phase 1A is more valuable than a partially expanded codebase.