# Pluralize Core

This is an updated package from [Pluralize.NET](https://github.com/sarathkcm/Pluralize.NET) so it can now be used for Net Core.

## What is Pluralize.Core?

(Originally) Pluralize.NET is a small C# library to pluralize and singularize English words.

## How to use it?

Package manage console:

```powershell
PM> Install-Package Pluralize.Core
```

Dotnet cli:

```shell
dotnet add package Pluralize.Core
```

Include the namespace:

```Csharp
using Pluralize.Core;
```

write the code:

```csharp
IPluralize pluralizer = new Pluralizer();

pluralizer.Singularize("Horses");  //=> "Horse"
pluralizer.Pluralize("Horse");  //=> "Horses"

// Example of new plural rule:
pluralizer.Pluralize("regex"); //=> "regexes"
pluralizer.AddPluralRule(new Regex("gex$"), "gexii");
pluralizer.Pluralize("regex"); //=> "regexii"

// Example of new singular rule:
pluralizer.Singularize('singles'); //=> "single"
pluralizer.AddSingularRule(new Regex("singles"), 'singular');
pluralizer.Singularize('singles'); //=> "singular"

// Example of new irregular rule, e.g. "I" -> "we":
pluralizer.Pluralize('irregular'); //=> "irregulars"
pluralizer.AddIrregularRule('irregular', 'regular');
pluralizer.Pluralize('irregular'); //=> "regular"

// Example of uncountable rule (rules without singular/plural in context):
pluralizer.Pluralize('paper'); //=> "papers"
pluralizer.AddUncountableRule('paper');
pluralizer.Pluralize('paper'); //=> "paper"

// Example of asking whether a word looks singular or plural:
pluralizer.IsPlural('test'); //=> false
pluralizer.IsSingular('test'); //=> true

// Example of formatting a word based on count
pluralizer.Format(5, "dog"); // => "dogs"
pluralizer.Format(5, "dog", inclusive: true); // => "5 dogs"
```

### Supported .Net Versions

- .Net Core 6.0
- .Net Core 7.0
- .Net Core 8.0

The library may work for other .Net versions, but as it is not maintained by the original author, I cannot ensure its compatibility with every version. Working with .Net 6, 7, and 8. If you found a compatibility problem, I appreciate [creating an issue in this repo](https://github.com/AshrafSada/PluralizeNetCore/issues/new/choose).

### License

Original license [MIT](https://github.com/sarathkcm/Pluralize.NET/blob/master/LICENCE).

### Contributors ::sparkles:

|![avatar](https://avatars.githubusercontent.com/u/5386817?v=4){width=150px}|![avatar](https://avatars.githubusercontent.com/u/2773690?v=4){width=150px}|![avatar](https://avatars.githubusercontent.com/u/14143311?v=4){width=150px}| ![avatar](https://avatars.githubusercontent.com/u/6270283?v=4){width=150px}|
|--|--|--|--|
|[Ashraf Sada](https://github.com/AshrafSada)|[Daniel Destouche](https://www.linkedin.com/in/daniel-destouche/)| [Sarath Kumar CM](https://github.com/sarathkcm)| [Dennis Pražák](https://sorashi.github.io/) |
