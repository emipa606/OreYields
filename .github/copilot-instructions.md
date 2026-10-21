# GitHub Copilot Instructions for Ore Yields (Continued)

## Mod Overview and Purpose

**Mod Name**: Ore Yields (Continued)

**Description**: Ore Yields (Continued) is an updated version of the original mod by Neceros. This mod is specifically designed for RimWorld and aims to enhance the mining experience by allowing players to customize the amount of materials dropped when mining various ores. Leveraging Harmony postfixes to ensure better compatibility with other mods, Ore Yields (Continued) now supports all minable resources, whether vanilla or modded. This provides players with greater control and flexibility over resource management in their gameplay experience.

- **Author**: Neceros (updated with permission by current maintainer)
- **Package ID**: Mlie.OreYields
- **License**: MIT License; Copyright (c) 2020 Billy Barnes

## Key Features and Systems

- **Adjustable Mining Yields**: Through the mod settings menu, players can set how much material is dropped when mining silver, gold, steel, plasteel, uranium, jade, and components.
- **Harmony Integration**: Utilizes Harmony postfixes for dynamic, reliable mod integration.
- **Compatibility**: Works with all minable resources, both from the base game and other mods.
- **Localization**: Includes Japanese localization provided by Proxyer.

## Coding Patterns and Conventions

- **C# Coding Conventions**: 
  - The mod structure follows the standard C# conventions for RimWorld modding.
  - Naming conventions use PascalCase for class and method names to conform to C# standards and maintain clarity across files.
  - Comments are used sparingly but purposefully to annotate complex logic or mod-specific implementations.

- **Project Structure**:
  - All C# files are located within the `Source\YieldPatch` directory.
  - Each file typically corresponds to a specific Harmony patch or settings initialization component.

## XML Integration

While this project primarily focuses on C#, integration with XML is minimal in this mod since it doesn’t involve direct XML def modification. The dynamic aspects, like mining yields, are purely controlled via C# and Harmony patches.

## Harmony Patching

- **Introduction to Harmony**: Harmony is a library used extensively in RimWorld modding for applying runtime modifications to pre-existing code (usually methods within the game's assemblies).

- **Postfix Example**: Postfix methods are employed to modify or extend behaviors after the original method execution. This minimizes conflicts by allowing other mods to still execute their logic with greater compatibility.

- **Types and Members**:
  - **OrePatch_Harmony**: Main Harmony patch class for modifying mining yield behaviors.
  - **Postfix Method Pattern**: Postfix methods are often suffixed with "_Postfix" or documented within classes like `StatExtension_GetStatValue` and `BuildingProperties_EffectiveMineableYield`.

## Suggestions for Copilot

- **Harmony Integration**: Utilize Copilot to suggest postfix templates for new methods to extend or modify behaviors without overwriting original methods.
- **Improving Compatibility**: Ask Copilot for suggestions on how to structure postfixes to handle edge cases with other mods, ensuring higher compatibility.
- **Code Comments**: Use Copilot to generate comments for the existing codebase where logic could benefit from additional documentation.
- **Error Handling**: Seek Copilot’s help in suggesting patterns for robust error handling, ensuring the mod gracefully handles exceptions during runtime.
- **Refactoring Suggestions**: Employ Copilot’s refactoring capabilities to optimize code for readability and maintainability, especially in performance-critical sections like those in mining calculations.

For additional support or to report issues, please use the Discord channel mentioned in the mod description and refer to the GitHub repository if you have solutions to bugs.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

