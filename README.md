# SFML.Net - Simple and Fast Multimedia Library for .Net

[SFML](https://www.sfml-dev.org) is a simple, fast, cross-platform and object-oriented multimedia API. It provides access to windowing,
graphics, audio and network.
It is originally written in C++, and this project is its official binding for .Net languages (C#, VB, ...).

## Authors

* Laurent Gomila - main developer (laurent@sfml-dev.org)
* Zachariah Brown - active maintainer (contact@zbrown.net)

## Download

You can get the latest official release on [NuGet](https://www.nuget.org/packages/SFML.Net/) or on [the
SFML website](https://www.sfml-dev.org/download/sfml.net).
You can also get the current development version from the [git repository](https://github.com/SFML/SFML.Net).

## Learn

There is no tutorial for SFML.Net, but since it's a binding you can use the C++ resources:
* [The official tutorials](https://www.sfml-dev.org/tutorials/)
* [The online API documentation](https://www.sfml-dev.org/documentation/)
* [The community wiki](https://github.com/SFML/SFML/wiki/)
* [The community forum](https://en.sfml-dev.org/forums/) (or [for French speakers](https://fr.sfml-dev.org/forums/))

Of course, you can also find the SFML.Net API documentation in the SDK.

## Dependencies

The NuGet package of SFML.Net comes with all dependencies, including native CSFML
and SFML libraries for most platforms.

For unsupported platforms or non-NuGet sources, you must have a copy of CSFML. CSFML can be compiled [from
source](https://github.com/SFML/CSFML/) or downloaded from [the official release
page](https://www.sfml-dev.org/download/csfml/). Also note that since CSFML depends on
the main SFML project you also need all SFML runtime dependencies.

Another dependency is the OpenTK library. This is required by the examples to run correctly.
It is not required unless you plan on running the example programs that are included.

## Contribute

SFML and SFML.Net are open-source projects, and they need your help to go on growing and improving.
Don't hesitate to post suggestions or bug reports on [the forum](https://en.sfml-dev.org/forums/)
or post new bugs/features requests on the [issue tracker](https://github.com/SFML/SFML.Net/issues/).
You can even fork the project on GitHub, maintain your own version and send us pull requests periodically to merge your work.
