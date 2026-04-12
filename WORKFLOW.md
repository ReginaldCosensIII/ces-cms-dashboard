# Workflow

## Branching
All implementation work must be completed on isolated `feat/` branches unless a different branch type is explicitly justified.

## Standard task flow
1. Define the task and scope in the project hub.
2. Review architecture and dependencies before implementation.
3. Generate or refine the implementation prompt.
4. Implement only the approved scope.
5. Manually verify all meaningful changes before staging and committing.
6. Stage, commit, and push only after verification.
7. Update relevant documentation before closing the task branch.

## Verification rule
No work should be committed and pushed until it has been manually reviewed and tested to a reasonable degree for the scope of the task.

## Scope control
Do not broaden tasks beyond the approved implementation slice.

## Agent expectations
Implementation agent summaries should clearly report:
- what changed
- files added or modified
- what was deferred
- blockers or assumptions
- mapping to done criteria