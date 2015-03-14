# Introduction #

`esecui` is a simple IDE for rapid development and testing of evolutionary algorithms  using [ESDL](http://stevedower.id.au/esdl). It provides a code editor, the ability to quickly view relevant results and export to [esec](http://code.google.com/p/esec).

`esecui` is not intended as a statistical workbench or similar. It is (deliberately) not possible to retrieve detailed histories or values from experiments; this level of analysis requires the use of [esec](http://code.google.com/p/esec) or another platform supporting ESDL.

The remainder of this document assumes familiarity with Evolutionary Computation (EC, [LMGTFY](http://www.google.com/search?ie=UTF-8&q=evolutionary+computation)) in general and [ESDL](http://stevedower.id.au/esdl) in particular.

# System #

The System tab displays the main code describing the algorithm. There are two parts to this code: the ESDL definition and the supporting Python code.

The `F1` key may be used at any time to display this tab.

## ESDL Definition ##

![![](http://stevedower.id.au/images/esecui/system_thumb.png)](http://stevedower.id.au/images/esecui/system.png)

The ESDL definition is the main algorithm. Everything else is provided to support this definition. For more information on writing an algorithm in ESDL, see [code.google.com/p/esec/wiki/ESDL](http://code.google.com/p/esec/wiki/ESDL), [stevedower.id.au/esdl](http://stevedower.id.au/esdl) and the publications at [stevedower.id.au/publications](http://stevedower.id.au/publications).

The second textbox (on the right-hand side) allows variable definitions to be separated from the system. For example, the variable `size` could be used throughout ESDL and a value specified separately. Variables specified here are made available for parameterisation when exporting.

At the bottom of this view is the error list. Compilation errors and warnings in any code in `esecui` are shown here; double-clicking an error will take you to its location. The `F4` key will check all syntax at any time, and starting an experiment while errors exist will instead display the error list.

## Python Support Code ##

![![](http://stevedower.id.au/images/esecui/python_thumb.png)](http://stevedower.id.au/images/esecui/python.png)

All species, generators and operators provided by `esec` are available for use in algorithms (see the Reference Guide at the [downloads](http://code.google.com/p/esecui/downloads/list) page), however, the strength of ESDL comes from being able to easily provide customised operators. The Python editor provides this.

Any class, variable or function will be shown in the list on the right, allowing simple navigation, as well as automatically included in the system context. For example, we could provide the following mutation operator in the Python editor:

```
def mutate_add(_source, amount=0):
    for indiv in _source:
        new_genes = [g + amount for g in indiv.genome]
        yield type(indiv)(genes=new_genes, parent=indiv)
```

(The `_source` parameter is required by `esec`: this is where the source stream (iterator) of individuals is provided. The `yield` statement is a standard pattern for `esec` individuals that does not restrict the type of individual that may be used. An `assert isinstance(...)` statement may be used to restrict type if necessary.)

The `mutate_add` operator can now be used as part of the ESDL system:

```
FROM parents SELECT offspring USING mutate_add(amount=1)
```

# Landscape #

![![](http://stevedower.id.au/images/esecui/landscape_thumb.png)](http://stevedower.id.au/images/esecui/landscape.png)

The Landscape tab displays the available problem landscapes that are available. All the landscapes provided by `esec` are available, categorised by the type of individual they are intended to be used with. The documentation associated with each landscape, if any, is shown to the right.

If parameters are available for the selected landscape they are shown with their default values to the right of the window.

The very last landscape category is simply "Custom". When selected, a new text box appears where a custom evaluator may be written (in Python). This code is the body of a function receiving a single individual as `indiv` and returning a fitness value.

```
fitness = 0.0
for x in indiv:
    fitness += x**2

return FitnessMinimise(fitness)
```

(Notice that the mutation operator used `indiv.genome` but the evaluator uses `indiv`. Individuals are not required to have identical genotypes and phenotypes: using the `genome` attribute ensures that the genotype is used, while `indiv` or `indiv.phenome` uses the phenotype.)

Fitness objects are either `FitnessMaximise` or `FitnessMinimise` (or equivalent US spelling). Specifying the object allows the sense of the fitness to be changed; integers or floating point numbers are by default converted to `FitnessMaximise`. Alternatively, another fitness object may be defined in the Python editor and used from the custom evaluator.

The `F2` key may be used at any time to display this tab. If you attempt to start an experiment without selecting a landscape, this tab will be automatically displayed.

# Results #

The Results tab provides the start/stop controls and three views.

![![](http://stevedower.id.au/images/esecui/chart_thumb.png)](http://stevedower.id.au/images/esecui/chart.png)

Experiments may be terminated based on the number of iterations, the number of function (evaluator) evaluations, the number of seconds or the best found fitness value. Termination criteria are only active when the checkbox is ticked (which is performed automatically in most cases).

The start button begins an experiment. If an error occurs while initialising the experiment, either the Log tab or the relevant location will be displayed. If an error occurs while running the experiment, in most cases it will simply terminate early with details available on the Log tab.

The `F3` key may be used to display this tab, with `Alt+1`, `Alt+2` and `Alt+3` available to switch between the views. Pressing `F5` to begin an experiment will switch to the Results tab.

## Chart ##

![![](http://stevedower.id.au/images/esecui/chart_thumb.png)](http://stevedower.id.au/images/esecui/chart.png)

The Chart view displays up to four fitness values from the experiment. The values available are:

  * the best ever observed fitness
  * the best fitness in the most recently observed group
  * the mean fitness of the most recently observed group
  * the worst fitness in the most recently observed group

(The group used here is the first one to be `YIELD`ed from the ESDL system. `esec` allows the group to be specified by name, but `esecui` assumes that the first group returned is the primary. Ensure that your algorithm yields the main group (typically `population`) first.)

Four checkboxes are available to disable any particular trace. The expression which converts the fitness to locations on the chart appear directly above the chart, and may be customised to suit a particular experiment.

The default expression is:

```
(i, f.simple)
```

which plots the "simple" value of each fitness (`f.simple`, simple meaning numeric and always maximising) against the iteration number (`i`). Each part of the fitness value may be displayed by substituting `f.values[#]`, where `#` is the index into the list of parts.

To define each trace independently, four tuples may be returned instead. For example, the expression

```
[(i, global_max.values[0]), (i, local_max.values[0]), (i, local_mean.values[0]), (i, local_min.values[0] / 10)]
```

shows the first part of each fitness and scales the worst fitness to 10% of its actual value.

The chart may be zoomed by clicking and dragging a rectangle around the area to zoom in on. Right-clicking will restore the original view, which autoscales to fit all the points included in the chart.

The `Alt+1` shortcut may be used to display this view when the Results tab is active.

## 2D Plot ##

![![](http://stevedower.id.au/images/esecui/vis_thumb.png)](http://stevedower.id.au/images/esecui/vis.png)

The 2D Plot view displays a point for each individual in the most recently `YIELD`ed group. The plot may be zoomed by clicking and dragging a rectangle around the area to zoom in on. Right-clicking will restore the original view, which autoscales to fit all the points.

The expression for the X and Y coordinates of each individual may be modified. The default expression is:

```
(indiv[0], indiv[1])
```

which uses the first two components of the individual as its X and Y coordinates.

To vary the size of each point, a third component may be included:

```
(indiv[0], indiv[1], len(indiv))
```

However, the size of each point does not vary with the zoom level.

The `Alt+2` shortcut may be used to display this view when the Results tab is active.

## Best ##

The Best view displays some attribute of the best seen individual as text. The expression used may be modified while the experiment is running or after it has completed in order to view different aspects.

The default expression is `indiv.phenome_string`, which is a textual representation of the individual's phenotype. Other expressions that are normally available include `indiv.genome` (a raw view of the individual's genotype), `indiv.fitness` or `indiv.fitness.values` (the fitness of the individual), `indiv.birthday` (the first iteration the individual was observed) or any Python expression. For example, the expression:

```
'\n'.join('%-20s:%20s' % (key, getattr(indiv, key)) for key in ('birthday', 'fitness', 'phenome_string'))
```

displays the birthday, fitness and phenome of an individual on separate lines:

```
birthday            :                3071
fitness             :              33.158
phenome_string      :    [-0.146, -4.815]
```

The `Alt+3` shortcut may be used to display this view when the Results tab is active.

# Log #

The Log tab displays output messages from `esec` or `esecui`. These typically consist of exception details and stack traces that are not otherwise handled (for example, by identifying an error in the system definition).

To report these errors, save your configuration, copy the entire text of the Log tab and attach both to an issue on our [Issues](http://code.google.com/p/esecui/issues/list) page.

# Export #

When you have finished designing and testing an algorithm, the Export command (under the Configuration menu, or `Ctrl+E`) will allow you to create a `.py` file suitable for use with `esec`s command line tool. See the `esec` [documentation](http://code.google.com/p/esec/w/list) for more details on running a batch file.

In the Export dialog, any variables specified separately from the system are available for parameterisation. These values may be specified as sets or combinations, defining a batch file that will run multiple times with varying parameters. The results from such a batch file may be used for statistical analysis.