# Circular Dependency

## Choosing an algorithm to search for cyclic dependence

I assume that initially I did not take into account the nuance of checking for cyclic dependence, because of this, a
rather heavy algorithm with complexity O(n²) turned out. If this really seriously affects performance in the future,
I plan to either rewrite this algorithm or make a feature in which it will be possible to indicate during binding that
verification is not required for heavy objects

***But at the moment I have come to two options for searching a Circular dependency***

## First solution

At the input, we get Bind Information of each parameter for a specific instance, in turn, Bind Information has a list of
ParametersExpression,
and we start checking whether each parameter does not contain an instance type in its parameters. It sounds wild, but it
came out in heavy 2 lines.

```c#
internal static bool HasCircularDependency(IEnumerable<BindInformation> parametersBindInformations,
                Dictionary<Type, IEnumerable<Type>> contractsInfo, Type instanceType,
                IEnumerable<ParameterExpression> parameterExpressionsOfInstance)
            {
                var isCircularArray = parametersBindInformations.Select(x => x.ParameterExpressions.Select(parameterExpression => parameterExpression.Type).Contains(type)).ToArray();
                if (isCircularArray.Contains(true))
                    throw JsExceptions.BindException.CircularDependency(type, type);
                    
                return false;
            }
```

## Second Solution

Here we do not know in what form a specific parameter will come to us, so we have to do an interface check and look for
a class with its implementation, fortunately we are looking in the dictionary, so here the complexity remains O(1). When
we already know what type of parameter turned out to be, we get its parameters, while taking them through the
information about the constructor, and not ready-made ones. Well, the matter remains small in the form of checking
whether the parameters of the instance, the instance itself

```c#
internal static bool HasCircularDependency(Dictionary<Type, IEnumerable<Type>> contractsInfo, Type instanceType,
                IEnumerable<ParameterExpression> parameterExpressionsOfInstance)
            {
                foreach (var param in parameterExpressionsOfInstance)
                {
                    var paramType = param.Type;
                    if (param.Type.IsInterface && contractsInfo.ContainsKey(param.Type))
                    {
                        paramType = contractsInfo[param.Type].Last();
                    }

                    var parametersOfParam = GetParametersExpression(paramType);
                    var map = Map(new[] { type }).First();
                    if (parametersOfParam.Where(x => x.Type == map.Type).ToArray().Length != 0)
                    {
                        throw JsExceptions.BindException.CircularDependency(type, paramType);
                    }
                }
                
            }
```

## Result

At the moment, I chose the second algorithm, it turned out to be faster in testing. Check the tests by date in the Json
file

* 11.05.2023 [JsonFile](https://github.com/jassus213/JSInjector/blob/main/JSInjector/Tests/CircularDependency/EfficiencyTest/EffiencyTestsCd.json);



