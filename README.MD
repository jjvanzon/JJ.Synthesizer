# JJ

*Jan-Joost van Zon, 2013-09-09*

## Introduction

JJ is the new code base.
The previous code base I began 3 years ago.

My ideas about architecture have changed.

The old code base is about a specific way of programming, not like mainstream .NET development.
The way of working is not portable between what I do at home and what I do at the office.

The new architecture is more agnostic about specific data models, specific programming models, specific API's and specific functionality.
The first order grouping will be by technicsal aspects such as apps, services, framework, validation, security, etcetera,
unlike the old code base which basically had a first-order grouping by functionality.

I feel that one architecture should accommodate many types of programming and many types of functionality in an orderly fashion.

## Set Up Dev Environment

* The solutions will only build if you clone another repository too: JJ.Framework. ([https://github.com/jjvanzon/JJ.Framework](https://github.com/jjvanzon/JJ.Framework))
* It must be cloned to a folder 'JJ.Framework' that is right next to your 'JJ' repository clone.
* First build JJ.Framework.sln or at least NuGet-restore it.
* Then build one of the solutions in this repository.
