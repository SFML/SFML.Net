# SFML.Net - Simple and Fast Multimedia Library for .Net

[SFML](https://www.sfml-dev.org) is a simple, fast, cross-platform and object-oriented multimedia API. It provides access to windowing,
graphics, audio and network.
It is originally written in C++, and this project is its official binding for .Net languages (C#, VB, ...).
The network module is not provided in the SFML.Net binding as .NET provides superior networking support.

## State of Development

Development is focused on version 3 in the `master` branch. No more features are planned for the 2.x release series.

## Authors

* Laurent Gomila (laurent@sfml-dev.org)
* Lukas Dürrenberger (eXpl0it3r@sfml-dev.org)
* Marioalexsan (mironalex@hotmail.com)
* Zachariah Brown (contact@zbrown.net)

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

The [SFML.Net NuGet package](https://www.nuget.org/packages/SFML.net) comes with all dependencies, including native CSFML
and SFML libraries for most platforms.

For unsupported platforms or non-NuGet sources, you must have a copy of CSFML. CSFML can be compiled [from
source](https://github.com/SFML/CSFML/) or downloaded from [the official release
page](https://www.sfml-dev.org/download/csfml/). Also note that since CSFML depends on
the main SFML project you also need all SFML runtime dependencies.

Some of the example projects also require the OpenTK library to run correctly, but it is not required for SFML.Net itself.

## Contribute

SFML and SFML.Net are open-source projects, and they need your help to go on growing and improving.
Don't hesitate to post suggestions or bug reports on [the forum](https://en.sfml-dev.org/forums/)
or post new bugs/features requests on the [issue tracker](https://github.com/SFML/SFML.Net/issues/).
You can even fork the project on GitHub, maintain your own version and send us pull requests periodically to merge your work.

## External libraries used by SFML.Net

* [CSFML](https://github.com/SFML/CSFML/) is under the zlib/png license
