# Workflow

This document defines the standardized development workflow for the CES CMS Dashboard project.

The goal is to keep development repeatable, reviewable, and professional while using an AI-assisted agentic process.

## Workflow goals

The workflow should ensure:
- clear scope control
- isolated and reviewable implementation work
- consistent documentation
- manual verification before commits and pushes
- repeatable task execution across future phases
- clean collaboration between the human lead, project hub, architect agent, and implementation agent

## Project roles

### Human lead
Responsible for:
- decision making
- scope approval
- QA and manual verification
- branch control
- environment verification
- final review before merge

### Project hub
Responsible for:
- planning
- scope control
- task breakdown
- documentation support
- implementation prompt generation
- review support
- milestone tracking

### Architect or system design agent
Responsible for:
- architecture review
- system design support
- technical audits
- implementation planning support
- risk identification
- design validation before execution

### Implementation agent
Responsible for:
- executing approved implementation work
- staying within the defined scope of the current branch
- updating relevant documentation
- returning clear summaries of completed work
- surfacing blockers or assumptions instead of hiding them

## Core workflow rules

### Branching rule
All implementation work must be completed on isolated feature branches unless a different branch type is explicitly justified.

Preferred naming pattern:
- `feat/...`

Examples:
- `feat/project-foundation`
- `feat/faq-crud-foundation`
- `feat/backend-faq-delivery`
- `feat/contact-faq-integration`

### Scope rule
Each branch should focus on one approved implementation slice.

Do not combine unrelated work.
Do not broaden a task because it seems convenient.
Do not introduce future-phase functionality early without approval.

### Verification rule
No meaningful work should be staged, committed, or pushed until it has been manually reviewed and verified to a reasonable degree for the scope of the task.

Manual verification may include:
- building the project
- running the application
- validating page behavior
- validating CRUD behavior
- checking migration output
- reviewing logs or console output
- confirming documentation accuracy

### Documentation rule
Relevant documentation must be updated as part of the same task when the implementation changes project structure, workflow, setup, architecture, or behavior.

### Honesty rule
Agents must not imply that work is verified if it was not verified.
If something could not be tested, that must be stated explicitly.

## Standard task execution flow

### Step 1. Define the task in the project hub
The task should be clarified in the project hub before implementation starts.

The hub should define:
- the task objective
- the implementation boundary
- what is in scope
- what is out of scope
- dependencies
- done criteria

### Step 2. Review architecture and dependencies
Before implementation begins, confirm:
- whether architecture review is needed
- whether adjacent repos are affected
- whether environment assumptions are known
- whether the task belongs in the current phase

### Step 3. Generate or refine the implementation prompt
The implementation prompt should be:
- narrow
- explicit
- phase-aligned
- clear about what is deferred
- clear about done criteria

### Step 4. Implement only the approved slice
The implementation agent should execute only the approved branch scope.

Do not:
- improvise major features
- refactor unrelated areas
- pull in future-phase concerns
- make architectural jumps without approval

### Step 5. Manually verify before staging and committing
Before any commit:
- review the changes
- verify the behavior
- confirm the scope stayed narrow
- confirm docs were updated where needed

### Step 6. Stage, commit, and push
After verification:
- stage the intended files
- create clear commits
- push the feature branch
- avoid noisy or misleading commit messages

### Step 7. Review the completion summary
The implementation agent should return a structured summary describing:
- what changed
- files changed
- what was deferred
- blockers or assumptions
- how the work maps to done criteria

### Step 8. Merge only after approval
Do not merge until:
- scope is reviewed
- implementation is verified
- documentation is acceptable
- follow-up issues are noted

## Standard implementation agent response format

Implementation agent summaries should clearly include:
1. Branch worked on
2. What was created or changed
3. Files and folders added or modified
4. Verification completed
5. What was intentionally deferred
6. Blockers or assumptions
7. Mapping to done criteria
8. Suggested next step or next branch

## Git hygiene expectations

### Commit quality
Commits should be:
- scoped
- descriptive
- honest
- easy to understand later

### Push quality
Only push work that is ready for review.
Do not push obviously broken or misleading states unless explicitly working in a draft/debug workflow.

### Merge quality
Prefer merging only after:
- manual review
- documentation review
- branch scope confirmation

## AI-assisted workflow guidance

The AI-assisted workflow is meant to improve speed and consistency, not to reduce rigor.

Use AI to:
- plan clearly
- implement faster
- document more consistently
- reduce repetition
- surface risks earlier

Do not use AI to:
- bypass verification
- hide uncertainty
- blur scope
- generate unnecessary complexity

## Local-only agent files and skills

Project-specific local agent notes or skills may exist locally for development convenience.

Rules:
- local-only agent files should remain untracked unless explicitly intended as repository documentation
- global reusable agent skills should remain outside the repo if they are part of the developer’s machine-level workflow
- the repo should only track files that provide actual project value

## Current practical workflow for this project

At the current stage:
- open only the `ces-cms-dashboard` folder in Antigravity for Phase 1A work
- keep adjacent repos available locally but out of the active implementation window
- avoid multi-root workspace usage until a later integration phase requires it
- keep the current implementation strictly inside this repository

## Final principle

A clean, narrow, reviewable implementation is more valuable than a broader but messy branch.

Stay controlled.
Stay honest.
Stay within scope.