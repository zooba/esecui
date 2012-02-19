#pylint: disable=C0103, C0302, R0201
# Disable: short variable names, too many lines, method could be a function

'''Real valued problem landscapes.

The `Real` base class inherits from `Landscape` for parameter validation
and support. See `landscape` for details.

'''

from math import sin, cos, fabs, sqrt, pi, e, exp, log
from itertools import izip
from esec.fitness import SimpleDominatingFitness
from esec.landscape import Landscape
from esec.utils import all_equal

#=======================================================================
class Real(Landscape):
    '''Abstract real-valued parameter fitness landscape
    '''
    ltype = 'RVP' # subclasses shouldn't change this
    ltype_name = 'Real-value'
    
    # This is universal for Real problems
    syntax = {
        'bounds': {
            'lower': [tuple, list, int, float, str, None],
            'upper': [tuple, list, int, float, str, None],
        },
        # lower_bounds overrules bounds.lower
        'lower_bounds?': [tuple, list, int, float, str, None],
        # upper_bounds overrules bounds.upper
        'upper_bounds?': [tuple, list, int, float, str, None],
    }
    
    # Subclasses can set default to overlay their changes on to this
    # Note that specifying defaults for lower_bounds or upper_bounds
    # will overrule any settings for bounds.lower or bounds.upper.
    default = {
        'bounds': { 'lower': -1.0, 'upper': 1.0 }
    }
    
    
    # used by testing to easily test each class using simple strings
    test_key = (('size.exact', int), ('bounds.lower', float), ('bounds.upper', float),)
    #n=params lbd ubd
    test_cfg = ('2 0.0 1.0',)
    
    
    def __init__(self, cfg=None, **other_cfg):
        '''Overlay syntax and default requirements, validate, assign
        range bounds and init bounds, and other landscape settings.
        '''
        # call super for overlaid syntax/defaults and validation
        super(Real, self).__init__(cfg, **other_cfg)
        
        # landscape bounds
        lbd = self.cfg.lower_bounds or self.cfg.bounds.lower
        if lbd is None: lbd = '-inf'
        if isinstance(lbd, (int, float, str)): lbd = [float(lbd)] * self.size.max
        assert len(lbd) >= self.size.max, 'At least %d lower bound values are required' % self.size.max
        
        ubd = self.cfg.upper_bounds or self.cfg.bounds.upper
        if ubd is None: ubd = 'inf'
        if isinstance(ubd, (int, float, str)): ubd = [float(ubd)] * self.size.max
        assert len(ubd) >= self.size.max, 'At least %d upper bound values are required' % self.size.max
        
        self.lower_bounds = lbd
        '''The inclusive lower range limit on each gene.
        
        Use `legal` on a genome to determine whether all genes are in
        the legal range.
        '''
        self.upper_bounds = ubd
        '''The exclusive upper range limit on each gene.
        
        Use `legal` on a genome to determine whether all genes are in
        the legal range.
        '''
    
    def legal(self, indiv):
        '''Check to see if an individual is legal.'''
        if not (self.size.min <= len(indiv) <= self.size.max):
            return False
        
        # Individual values are checked by indiv.legal(). We simply
        # check here whether the individual has bounds equal to or
        # within ours.
        return (all(i1 >= i2 for i1, i2 in izip(indiv.lower_bounds, self.lower_bounds)) and
                all(i1 <= i2 for i1, i2 in izip(indiv.upper_bounds, self.upper_bounds)))
    
    
    def info(self, level):
        '''Return landscape info for any real landscape.'''
        result = []
        part = "Using landscape %s" % self.lname
        if self.invert:
            part += " (inverted)"
        if self.offset:
            part += " with offset %f" % self.offset
        if self.size.exact:
            part += " defined on %d parameter(s)" % self.size.exact
        else:
            part += " defined on [%d, %d) parameters(s)" % (self.size.min, self.size.max)
        result.append(part)
        
        if level > 0:
            # parameter bounds
            result.append("with parameter bounds of:")
            result.extend(self._bounds_info(self.lower_bounds, self.upper_bounds))
        
        result.extend(super(Real, self).info(level)[1:])
        
        return result
    
    def _bounds_info(self, lbd, ubd):
        '''Return bounds (less verbosely if all common).'''
        n = len(lbd)
        result = []
        if n > 5 and all_equal(lbd) and all_equal(ubd):
            for i in xrange(0, 2):
                result.append(" %3d: % 10.8f  % 10.8f" % ((i+1), lbd[0], ubd[0]))
            result.append("    :    ...         ...    ")
            result.append(" %3d: % 10.8f  % 10.8f" % (n, lbd[0], ubd[0]))
        else:
            for i in xrange(n):
                result.append(" %3d: % 10.8f  % 10.8f" % ((i+1), lbd[i], ubd[i]))
        return result


#=======================================================================
# Algorithm Test Standards: Linear, Neutral, Stabilising and Disruptive!
#=======================================================================

#=======================================================================
class Linear(Real):
    '''n-dimensional linear landscape for algorithm testing. This is the
    real value equivalent to binary OneMax and integer Nsum problems for
    n dimensions.
    
    Standard initialisation range of [0, 100]
    
    Maximum vector of x=[100, 100, ..., 100] results in f(x)=100*n
    
    Minimum vector of x=[0, 0, ..., 0] results in f(x)=0
    
    Qualities: maximisation, separable, unconstrained
    '''
    lname = 'Linear'
    
    default = { 'size': { 'min': 10, 'max': 10 }, 'bounds': { 'lower': 0.0, 'upper': 100.0 } }
    
    test_cfg = ('2 0.0 100.0',)
    
    def _eval(self, indiv):
        '''Returns the sum of all values'''
        return sum(indiv)


#=======================================================================
class Neutral(Real):
    '''n-dimensional constant value landscape of 1.0 for algorithm
    testing. Used mainly for testing drift as fitness is not linked to
    gene value.
    
    Standard initialisation range of [0, 100]
    Output of f(x) is constant (``mean``), f(x) = 1.0
    
    Qualities: neither maximisation and minimisation, unconstrained
    '''
    lname = 'Neutral (constant)'
    
    syntax = { 'mean': float }
    default = {
        'size': { 'min': 1, 'max': 1 },
        'bounds': { 'lower': 0.0, 'upper': 100.0 },
        'mean': 1.0
    }
    
    test_cfg = ('2 0.0 100.0',)
    
    def __init__(self, cfg=None, **other_cfg):
        super(Neutral, self).__init__(cfg, **other_cfg)
        self.mean = self.cfg.mean
    
    def _eval(self, indiv):         #pylint: disable=W0613
        '''Returns the value of ``mean``.'''
        return self.mean
    

#=======================================================================
class Stabilising(Real):
    '''n-dimensional stabilising value landscape for algorithm testing.
    Used for illustrating standard selection pressure effects.
    
    Output of f(x) a Cauchy-Lorentz distribution of peak amplitude I,
    centred around the mean x0 (default 50) of the specified range. The
    scale parameter gamma controls the "spead" of the distribution
    around the mean.
    
    Defaults
    - initialisation range of [0, 100]
    - amplitude I of 1.0
    - mean x0 of 50.0 (f(x0) == I == 1.0)
    - gamma of 10.0 (spread value ~ 1/10th range)
    
    Qualities: maximisation, separable, unconstrained
    '''
    lname = 'Stabilising'
    
    syntax = { 'mean': float, 'amplitude': float, 'gamma': float }
    default = {
        'size': { 'min': 1, 'max': 1 },
        'bounds': { 'lower': 0.0, 'upper': 100.0 },
        'mean': 50.0,
        'amplitude': 1.0,
        'gamma': 10.0
    }
    
    test_cfg = ('2 0.0 100.0',)
    
    def __init__(self, cfg=None, **other_cfg):
        super(Stabilising, self).__init__(cfg, **other_cfg)
        self.gamma_sq = self.cfg.gamma ** 2
        self.mean = self.cfg.mean
        self.amp = self.cfg.amplitude
    
    def _eval(self, indiv):
        '''f(x) = I * ( gamma^2 / ((x - mean)^2 + gamma^2))'''
        result = 0.0
        g_sq = self.gamma_sq
        m = self.mean
        I = self.amp
        for x in indiv:
            result += I * (g_sq / ((m-x)**2 + g_sq))
        return result

#=======================================================================
class Disruptive(Real):
    '''n-dimensional disruptive value landscape for algorithm testing.
    Used for illustrating standard selection pressure effects.
    
    A negative form of the `Stabilising` landscape.
    
    Defaults
    - initialisation range of [0, 100]
    - amplitude I of 1.0
    - mean x0 of 50.0 (f(x0) == I == 1.0)
    - gamma of 10.0 (spread value ~ 1/10th range)
    
    Qualities: maximisation, separable, unconstrained
    '''
    lname = 'Stabilising'
    
    syntax = { 'mean': float, 'amplitude': float, 'gamma': float }
    default = {
        'size': { 'min': 1, 'max': 1 },
        'bounds': { 'lower': 0.0, 'upper': 100.0 },
        'mean': 50.0,
        'amplitude': 1.0,
        'gamma': 10.0
    }
    
    test_cfg = ('2 0.0 100.0',)
    
    def __init__(self, cfg=None, **other_cfg):
        super(Disruptive, self).__init__(cfg, **other_cfg)
        self.gamma_sq = self.cfg.gamma ** 2
        self.mean = self.cfg.mean
        self.amp = self.cfg.amplitude
    
    def _eval(self, indiv):
        '''f(x) = I - I * ( gamma^2 / ((x - mean)^2 + gamma^2))'''
        result = 0.0
        g_sq = self.gamma_sq
        m = self.mean
        I = self.amp
        for x in indiv:
            result += I - I * (g_sq / ((m-x)**2 + g_sq))
        return result

#=======================================================================
# Optimisation Standards
#=======================================================================


#=======================================================================
class Sphere(Real):
    '''n-dimensional spherical (parabola or parabolic) benchmark problem
    landscape.
    
    A classic benchmark problem also known as De Jong Function 1 (F1)
    (De Jong, 1975) and also used in early ES studies. Originally
    defined for two dimensional and later generalised for N.
        
        f(x) = sum((x_i)^2)
    
    Note: Minimisation by default. Often inverted and offset for use as
    a fitness value (as with many other "optimisation" functions).
    
    Standard initialisation range of [-5.12, 5.12]
    Minimum vector of x=[0, 0, ..., 0] results in f(x)=0
    
    Qualities: minimisation, unimodal, additively separable, unconstrained
    '''
    lname = 'Sphere'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -5.12, 'upper': 5.12 } }
    
    test_cfg = ('2 0.0 100.0',)
    
    def _eval(self, indiv):
        '''f(x) = sum((x_i)^2)
        '''
        return sum(v*v for v in indiv)
    

#rename Parabola (EC) to the more common standard Sphere
Parabola = Sphere


#=======================================================================
class Ellipsoid(Real):
    '''Ellipsoid benchmark problem landscape.
    
    A simple unimodal surface, also known as the "axis parallel
    ellipsoid function". Essentially the same as the `Sphere` problem,
    except that it is "stretched" along each additional dimensional axis
    (creating a different gradient for each dimension). See also
    `HyperEllipsoid`.
        
        f(x) = sum(i*(x_i)^2)
    
    Standard initialisation range of [-5.12, 5.12]
    Minimum vector of x=[0, 0, ..., 0] results in f(x)=0
    
    Qualities: minimisation, unimodal, additively separable,
               unconstrained
    '''
    lname = 'Ellipsoid'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -5.12, 'upper': 5.12 } }
    
    test_cfg = ('2 -5.12 5.12',)
    
    def _eval(self, indiv):
        '''f(x) = sum(i^2 * x(i)^2)'''
        return sum(((i+1) * x*x) for i, x in enumerate(indiv))


#=======================================================================
class HyperEllipsoid(Real):
    '''Hyper Ellipsoid benchmark problem landscape.
    
    A simple convex unimodal surface, also known as the "axis parallel
    hyperellipsoid function". Essentially the same as the Sphere
    problem, except that it is "stretched" along each additional
    dimensional axis (creating a different gradient for each dimension).
    
    Standard initialisation range of [-5.12, 5.12]
    Minimum vector of x=[0, 0, ..., 0] results in f(x)=0
    
    Qualities: minimisation, unimodal, additively separable,
               unconstrained
    '''
    lname = 'Hyper Ellipsoid'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -5.12, 'upper': 5.12 } }
    
    test_cfg = ('2 -5.12 5.12',)
    
    def _eval(self, indiv):
        '''f(x) = sum(i^2 * x(i)^2)
        '''
        return sum(((i+1)**2 * x*x) for i, x in enumerate(indiv))


#=======================================================================
class Quadric(Real):
    '''Quadric benchmark problem landscape
    
    Similar to the stretched-sphere qualities of the hyperellipsoid
    function and also rotated. This version is also known as "Schwefel's
    function 1.2", "Schewefel's Double Sum" and the "rotated
    hyperellipsoid function".
        
        f() = sum_{i=1 }^{n }( (sum_{j=1 }^{i }{x_j })^2)
    
    Other variations exists that also offset each axis in proportion to
    the dimension indices, which is of use to some specific research
    questions.
    
    Standard initialisation range of [-65.536, 65.536]
    Minimum vector of x=[0, 0, ..., 0] results in f(x)=0
    
    Qualities: minimisation, unimodal, non-separable, unconstrained
    '''
    lname = 'Quadric (Rotated Hyper-ellipsoid)'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -65.536, 'upper': 65.536 } }
    
    test_cfg = ('2 -65.536 65.536',)
    
    def _eval(self, indiv):
        '''f() = sum_{i=1 }^{n }( (sum_{j=1 }^{i }{x_j })^2)'''
        total = 0
        for i in xrange(len(indiv)):
            tmp = 0
            for j in xrange(i+1):
                tmp += indiv[j]
            total += tmp**2
        return total
RotatedHyperEllipsoid = Quadric

#=======================================================================
class NoisyQuartic(Real):
    '''n-dimensional quartic function landscape with added Gaussian
    noise.
    
    No fixed global optimum due to the noise, but on-average the same as
    the quartic function. An interesting test case for the robustness
    qualities of some algorithms when faced with noise.
    
    Gaussian noise (seed) is used. Other random distributions could be
    used.
    
    Standard initialisation range of [-5.12, 5.12]
    Minimum vector of x=[0, 0, ..., 0] results approximately f(x)=0
    
    Qualities: maximisation, ~unimodal, non-separable, unconstrained
    '''
    lname = 'Noisy Quartic'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -5.12, 'upper': 5.12 } }
    
    test_cfg = ('2 -5.12 5.12',)
    
    def _eval(self, indiv):
        '''f(x) = sum(i*(x_i)^4 + gauss(0, 1))'''
        gauss = self.rand.gauss
        return sum((i+1) * x**4 + gauss(0, 1) for i, x in enumerate(indiv))


#=======================================================================
class Easom(Real):
    '''Easom benchmark function landscape
    
    A very specific 2D problem domain. It distinctive qualities are that
    it contains very little gradient "hints" that would be of use to
    hill-climbing or local-optimisation techniques, and a dramatic
    unimodal optimum region at (pi, pi). The standard value range used
    is [-100, 100].
        
        f(x) = cos(x1)*cos(x2)*exp(-((x1-pi)^2+(x2-pi)^2))
    
    When used as a maximisation function, the output is normalised
    (0, 1). However the function is often inverted for use as
    minimisation benchmark, and any offset could be specified.
    
    Standard initialisation range of [-100, 100]
    Maximum vector of x=[pi, pi] results in f(x)=1.0
    
    If inverted (for minimisation), an offset of 1.0 is a suggested
    standard value.
    
    Qualities: maximisation, unimodal, normalised, (un)constrained
    '''
    lname = 'Easom'
    
    default = { 'size': { 'exact': 2 }, 'bounds': { 'lower': -100.0, 'upper': 100.0 } }
    strict = { 'size.exact': 2 }
    
    test_cfg = ('2 -100 100',)
    
    def _eval(self, indiv):
        '''f(x) = cos(x1)*cos(x2)*exp(-((x1-pi)^2+(x2-pi)^2))'''
        x1, x2 = indiv
        result = cos(x1) * cos(x2) * exp(-((x1-pi)**2 + (x2-pi)**2))
        return result

#=======================================================================
class Rosenbrock(Real):
    '''Overlapping n-dimensional Rosenbrock benchmark function
    landscape.
    
    A very well known classic optimisation problem, with many
    alternative titles including "De Jong Function 2 (F2)",
    "Rosenbrock's Saddle" and the "Banana" function. The 2D version is
    often shown, however there can be issues comparing n-dimensional
    versions of this function, as some implementations simply use a sum
    of 2D pairs, while this version is the tougher sum of overlapping
    pairs version.
    
    See: http://mathworld.wolfram.com/RosenbrockFunction.html for 2D
    details.
    
    Standard initialisation range of [-2.048, 2.048]
    Minimum vector of x=[1, 1, ..., 1] results in f(x)=0
    
    Qualities: minimisation, multimodal (n>2), non-separable,
               unconstrained
    '''
    lname = 'Rosenbrock'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -2.048, 'upper': 2.048 } }
    
    test_cfg = ('2 -2.048 2.048', '10 -2.048 2.048')
    
    def __init__(self, cfg=None, **other_cfg):
        super(Rosenbrock, self).__init__(cfg, **other_cfg)
    
    def _eval(self, indiv):
        '''n-dimensional case'''
        total = 0
        x = indiv[0]
        for y in indiv[1:]:
            total += (1-x)*(1-x) + 100*(y-x*x)*(y-x*x)
            x = y
        return total

#=======================================================================
class Rastrigin(Real):
    '''Rastrigin benchmark problem landscape
    
    Similar to the Sphere (De Jong F1), however the surface is modulated
    with an additional cosine term to induce multiple local minima, and
    hence create a highly multimodal and deceptive surface. Modal
    features are regular and separable.
        
        f(x) = 10.0*n + sum(x(i)^2 - 10.0*cos(2*pi*x(i)))
    
    In this instance the standard values of amplitude A=10 and
    modulation frequency \omega=2*pi. If altered, this landscape can be
    used as a simple problem generator, although this is rarely done.
    
    The default setting is to use an inverted landscape for
    maximisation.
    
    Standard initialisation range of [-5.12, 5.12]
    Minimum vector of x=[0, 0, ..., 0] results is f(x)=0
    
    Qualities: minimisation, multimodal, not normalised, unconstrained
    '''
    lname = 'Rastrigin'
    maximise = False
    
    default = { 'size': { 'exact': 2 }, 'bounds': { 'lower': -5.12, 'upper': 5.12 } }
    strict = { 'size.exact': '*' }
    
    test_cfg = ('2 -5.12 5.12',)
    
    def _eval(self, indiv):
        '''f() = 10*n + sum((x_i)^2 - 10cos(2*pi*x_i))'''
        c = 2*pi
        return 10*len(indiv) + sum( x*x - 10*cos(c*x) for x in indiv)

#=======================================================================
class Griewangk(Real):
    '''Griewangk benchmark problem landscape
    
    The surface of this domain is similar to the Rastrigin function, in
    that it creates a sphere-like surface modulated by a cosine based
    term. There are many local optima regularly distributed. At large
    scale the overall surface is relatively smooth, while at small scale
    near the optimum, the modulation term creates a very deceptive
    surface.
        
        f(x) = 1/4000*sum(x_i-100)^2 - prod((x_i-100)/sqrt(i)) + 1
    
    Not recommended as high-dimensional standard (as it becomes smoother
    and less deceptive as n increases).
    
    Standard initialisation range of [-600, 600]
    Minimum vector of x=[0, 0, ..., 0] results in f(x)=0.
    
    Qualities: minimisation, multimodal, non-separable, (un)constrained.
    '''
    lname = 'Griewangk'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -600., 'upper': 600. } }
    strict = { 'size.exact': '*' }
    
    test_cfg = ('2 -600 600',)
    
    def _eval(self, indiv):
        '''f(x) = 1 + sum(x_i^2/4000) - prod(code(x_i/sqrt(i))'''
        total = 0
        prod = 1
        for i, x in enumerate(indiv):
            total += x*x
            prod *= cos(x/sqrt(i+1))
        return 1 + (total / 4000.) - prod



#=======================================================================
class Ackley(Real):
    '''Ackley benchmark problem landscape
    
    Also known as "Ackley's Path". Originally defined for two
    dimensions, later generalised to n. The overall form is an
    exponential well modulated by a cosine term providing the familiar
    macro- and micro-level challenge of many benchmark problems. Unlike
    the Rastigin function, Ackley's is not separable despite the
    appearance of regular local optima.
        
        f(x) = 20 + e - 20exp(-0.2 * sqrt((1/n)*sum(x_i^2)))
                      - exp((1/n)*sum(cos(2*pi*x_i)))
    
    Although it is possible to alter the constant terms of the function
    and use this as a problem generator, it seems this is rarely done in
    practice.
    
    Standard initialisation range of [-32.768, 32.768] or [-30, 30]
    Minimum vector of x=[0, 0, ..., 0] results in f(x)=0.
    
    Qualities: minimisation, multimodal, non-separable, not normalised.
    '''
    lname = 'Ackley'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -30.0, 'upper': 30.0 } }
    strict = { 'size.exact': '*' }
    
    test_cfg = ('2 -30.0 30.0',)
    
    def _eval(self, indiv):
        '''f(x) = 20 + e - 20exp(-0.2 * sqrt((1/n)*sum(x_i^2)))
                         - exp((1/n)*sum(cos(2*pi*x_i)))
        '''
        n = float(len(indiv))
        c = 2*pi
        s1 = s2 = 0
        for x in indiv:
            s1 += x*x
            s2 += cos(c*x)
        return -20 * exp(-0.2*sqrt((1/n)*s1)) - exp((1/n)*s2) + 20 + e



#=======================================================================
class Schwefel(Real):
    '''Schwefel benchmark problem landscape (Sine Root)
    
    A deceptive multimodal minimisation problem with a single global
    minimum geometrically distant fromt the best known local minimum
    within the constrained search space, making it very likely for
    searches to converge to non-optimal locations.
        
        f(x) = 418.9829*n + sum(x_i * sin(sqrt(abs(x_i))))
    
    The offset term of 418.9829*n is used to create an optimum f(x) = 0
    
    Standard initialisation of [-500, 500] or [-512, 512] or
    [-512.03, 511.97]
    
    Minimum vector of x=[-420.9687, -420.9687, ..., -420.9687] results
    in f(x)=0.
    
    Qualities: minimisation, multimodal, additively separable,
               (un)constrained.
    '''
    lname = 'Schwefel'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': -512.0, 'upper': 511.0 } }
    strict = { 'size.exact': '*' }
    
    test_cfg = ('2 -512 512',)
    
    def _eval(self, indiv):
        '''f(x) = 418.9829*n + sum(x_i * sin(sqrt(abs(x_i))))'''
        return 418.9829*len(indiv) + sum(x * sin(sqrt(fabs(x))) for x in indiv)


#=======================================================================
class Michalewicz(Real):
    '''Michalewicz benchmark function landscape.
    
    A multimodal domain with n! local optima and very little gradient
    information for guided local search methods to take advantage of,
    although the domain is additively separable.
        
        f(x) = -sum(sin(x_i)*sin(i*x_i^2 / pi)^(2*m))
    
    Standard initialisation range of [0, pi]
    Minimum results reported for m = 10 for different n values::
        
        n=2,  f(x) = -1.8013
        n=5,  f(x) = -4.687658
        n=10, f(x) = -9.66015
    
    Qualities: minimisation, multimodal, additively separable,
               unconstrained
    '''
    lname = 'Michalewicz'
    maximise = False
    
    default = { 'size': { 'min': 2, 'max': 2 }, 'bounds': { 'lower': 0, 'upper': pi } }
    strict = { 'size.exact': '*' }
    
    test_cfg = ('2 0.0 3.1416',)
    
    def __init__(self, cfg=None, **other_cfg):
        super(Michalewicz, self).__init__(cfg, **other_cfg)
        self.m2 = 2*10 # this is a default standard
    
    def _eval(self, indiv):
        '''f(x) = -sum(sin(x_i)*sin(i*x_i^2 / pi)^(2*m))'''
        m2 = self.m2
        total = 0
        for i, x in enumerate(indiv):
            total += sin(x)*sin(((i+1)*x*x)/pi)**m2
        return -total



#=======================================================================
# Multimodal Problem Landscape
#=======================================================================

#=======================================================================
class MultiPeak1(Real):
    '''Sinusoidal Multiple Peaks landscape (for niching).
    
    Part 1 of a set of four one-dimensional multipeak functions for
    standard niche method testing.
        
        f(x) = sin^6(5*pi*x)
    
    This version:
    - Peaks of equal height
    - Regular spacing between peaks
    
    Five equivalent maxima of f(x) = 1.0 at x values of
    [0.1, 0.3, 0.5, 0.7, 0.9]
    
    Qualities: maximisation, multimodal, normalised, constrained.
    '''
    lname = 'Multipeak1'
    
    default = { 'size': { 'exact': 1 }, 'bounds': { 'lower': -0.0, 'upper': 1.0 } }
    strict = { 'size.exact': 1 }
    
    test_cfg = ('1 -0.0 1.0',)
    
    def _eval(self, indiv):
        '''f(x) = sin^6(5*pi*x)
        '''
        return sin(5*pi*indiv[0])**6

#=======================================================================
class MultiPeak2(Real):
    '''Sinusoidal Multiple Peaks (for niching).
    
    Part 2 of a set of four one-dimensional multipeak functions for
    standard niche method testing.
        
        f(x) = sin^6(5*pi(x^(3/4)-0.05))
    
    This version:
    - Peaks of equal height
    - Varyied spacing (expansion) between peaks
    
    Qualities: maximisation, multimodal, normalised, constrained.
    '''
    lname = 'Multipeak2'
    
    default = { 'size': { 'exact': 1 }, 'bounds': { 'lower': -0.0, 'upper': 1.0 } }
    strict = { 'size.exact': 1 }
    
    test_cfg = ('1 -0.0 1.0',)
    
    def _eval(self, indiv):
        '''f(x) = sin^6(5*pi(x^(3/4)-0.05))
        '''
        return sin(5*pi*(indiv[0]**(3.0/4)-0.05))**6

#=======================================================================
class MultiPeak3(Real):
    '''Decreasing Multiple Peaks (for niching).
    
    Part 3 of a set of four one-dimensional multipeak functions for
    standard niche method testing.
        
        f(x) = (exp(-2*log(2)*((x-0.08)/0.854)**2))*sin(5*pi*x)**6
    
    This version:
    - Peaks of decreasing height (single maxima)
    - Regular spacing of peaks
    
    Qualities: maximisation, multimodal, normalised, constrained.
    '''
    lname = 'Multipeak3'
    
    default = { 'size': { 'exact': 1 }, 'bounds': { 'lower': -0.0, 'upper': 1.0 } }
    strict = { 'size.exact': 1 }
    
    test_cfg = ('1 -0.0 1.0',)
    
    def _eval(self, indiv):
        '''f(x) = (exp^(...))*sin^6(5*pi*x)
        '''
        x = indiv[0]
        return (exp(-2*log(2)*((x-0.08)/0.854)**2))*sin(5*pi*x)**6

#=======================================================================
class MultiPeak4(Real):
    '''Decreasing Multiple Peaks (for niching).
    
    Part 3 of a set of four one-dimensional multipeak functions for
    standard niche method testing.
        
        f(x) = (exp(-2*log(2)*((x-0.08)/0.854)**2)) * 
               sin(5*pi*(x**(3.0/4)-0.05))**6
    
    This version:
    - Peaks of decreasing height (single optima)
    - Varyied spacing (expansion) between peaks
    
    Qualities: maximisation, multimodal, normalised, constrained.
    '''
    lname = 'Multipeak4'
    
    default = { 'size': { 'exact': 1 }, 'bounds': { 'lower': -0.0, 'upper': 1.0 } }
    strict = { 'size.exact': 1 }
    
    test_cfg = ('1 -0.0 1.0',)
    
    def _eval(self, indiv):
        '''f(x) = (exp^(...))*sin^6(5*pi*x)
        '''
        x = indiv[0]
        return (exp(-2*log(2)*((x-0.08)/0.854)**2))*sin(5*pi*(x**(3.0/4)-0.05))**6


#=======================================================================
class Booth(Real):
    '''Booth two-dimensional function landscape.
    
    A simple constrained two-dimensional multimodal (subtle) domain
    containing several local minima and one global minimum.
        
        f(x1, x2) = (x_1 + 2*x_2 - 7)^2 + (2*x_1 + x_2 -5)^2
    
    Standard initialisation range of [-10, 10]
    Minimum vector of x=[1, 3] results in f(x)=0.
    
    Qualities: minimisation, multimodal, non-separable, constrained.
    '''
    lname = 'Booth'
    maximise = False
    
    default = { 'size': { 'exact': 2 }, 'bounds': { 'lower': -10.0, 'upper': 10.0 } }
    strict = { 'size.exact': 2 }
    
    test_cfg = ('2 -10.0 10.0',)
    
    def _eval(self, indiv):
        '''f(x1, x2) = (x_1 + 2*x_2 - 7)^2 + (2*x_1 + x_2 -5)^2
        '''
        x1, x2 = indiv
        return (x1 + 2*x2 - 7)**2 + (2*x1 + x2 - 5)**2


#=======================================================================
class Himmelblau(Real):
    '''Himmelblau two-dimensional function landscape.
    
    A two-dimensional multimodal minimisation problem landscape with
    four near equal optimum (although there are differences at higher
    value resolution).
        
        f(x) = (x1^2 + x2 - 11)^2 + (x1 + x2^2 - 7)^2
    
    Standard initialisation range of [-5, 5]
    Minimum 2D vectors resulting in f(x)~=0 are:
        
        f([ 3.00, 2.00]) = 0
        f([ 3.58, -1.85]) = 0.0011
        f([-3.78, -3.28]) = 0.0054
        f([-2.81, 3.13]) = 0.0085
    
    Qualities: minimisation, multimodal, non-separable, constrained.
    '''
    lname = 'Himmelblau'
    maximise = False
    
    default = { 'size': { 'exact': 2 }, 'bounds': { 'lower': -5.0, 'upper': 5.0 } }
    strict = { 'size.exact': 2 }
    
    test_cfg = ('2 -5.0 5.0',)
    
    def _eval(self, indiv):
        '''f(x) = (x1^2 + x2 - 11)^2 + (x1 + x2^2 - 7)^2
        '''
        x1, x2 = indiv
        return (x1**2 + x2 - 11)**2 + (x1 + x2**2 - 7)**2


#=======================================================================
class SixHumpCamelBack(Real):
    '''Six Hump Camel-back function landscape.
    
    A two-dimensional non-separable multimodal and multi-solution
    minimisation problem with six minimum features within an asymmetric
    bounded domain.
        
        f(x) = 4x1^2 - 2.1x1^4 + (1/3)x1^6 + x1*x2 - 4x2^2 + 4x2^4
    
    Constrained initialisation range of x1 [-3, 3] and x2 [-2, 2]
    Minimum 2D vectors of::
        
        f([-0.08983, 0.7126]) = -1.0316
        f([ 0.08983, -0.7126]) = -1.0316
    
    Qualities: maximisation, multimodal, non-separable, constrained
    '''
    lname = 'Six Hump Camel-back'
    maximise = False
    
    default = {
        'size': { 'exact': 2 },
        'lower_bounds': [-3.0, -2.0],
        'upper_bounds': [ 3.0, 2.0],
    }
    strict = { 'size.exact': 2, 'lower_bounds': [-3.0, -2.0], 'upper_bounds': [3.0, 2.0] }
    
    test_cfg = ('2',)
    
    
    def _eval(self, indiv):
        '''f(x) = 4x1^2 - 2.1x1^4 + (1/3)x1^6 + x1*x2 - 4x2^2 + 4x2^4
        '''
        x1, x2 = indiv
        result = 4*(x1**2) - 2.1*(x1**4) + (1.0/3.0)*(x1**6) + x1*x2 - 4*(x2**2) + 4*(x2**4)
        return result

#=======================================================================
class SchafferF6(Real):
    '''Schaffer's two-dimensional f6 benchmark function.
    
    A two-dimensional deceptive minimisation problem landscape that is
    mostly flat except near the global minimum.
        
        f(x, y) = 0.5 + (sin^2(sqrt(x^2+y^2)) - 0.5) / 
                        (1+0.001(x^2+y^2))^2
    
    Standard initialisation range of [-100, 100]
    The global minimum of 0 is located at (0, 0).
    
    Qualities: minimisation, non-separable, constrained.
    '''
    lname = "Schaffer's F6"
    maximise = False
    
    default = { 'size': { 'exact': 2 }, 'bounds': { 'lower': -100.0, 'upper': 100.0 } }
    strict = { 'size.exact': 2 }
    
    test_cfg = ('2 -100.0 100.0',)
    
    def _eval(self, indiv):
        '''f(x, y) = 0.5 + (sin^2(sqrt(x^2+y^2)) - 0.5) / 
                           (1+0.001(x^2+y^2))^2
        '''
        x, y = indiv
        d = x*x+y*y
        return 0.5 + (sin(sqrt(d))**2 - 0.5) / (1 + 0.001*d)**2

#=======================================================================
# Real-valued Landscape Generators
#=======================================================================


#=======================================================================
class FMS(Real):
    '''Frequency Modulation Sounds landscape generator.
    
    A highly complex multimodal function with strong epistasis. The
    objective is to determine six real value parameters used in a FM
    sound model. \cite{Tsutsui1993, Tsutsui1997 }
    
    See http://tracer.lcc.uma.es/problems/fms/fms.html
    
    The sound model is defined as:
        
        y(t) = a1 * sin(w1*t*theta + a2 * 
                        sin(w2*t*theta + a3 * sin(w3*t*theta)))
    
    where a set of values for (a1, w1, a2, w2, a3, w3) has been
    specified. A standard set of values has been implemented, but other
    values could also be used to make this function a landscape
    generator:
        
        x_0 = (1.0, 5.0, -1.5, 4.8, 2.0, 4.9)
    
    All values are in the range of [-6.4, 6.35]
    
    The output of the function using the reference values y_0 is then
    sample at 100 points over an interval of 2*pi and compared to the
    function output using a set of supplied model parameters. The error
    is squared and summed.
        
        f(x) = sum((y(t) - y_0(t))^2), for 100 points of t in [0, 2*pi]
    
    The minimum error vector x (matching the x_0 values) gives f(x) = 0
    
    Difficult (very) to solve without local search so it has been
    suggested to stop at around 10^-2 resolution.
    
    Qualities: minimisation (error), multimodal, non-separable,
               constrained
    '''
    lname = 'Frequency Modulation Sounds (FMS)'
    maximise = False
    default = { 'size': { 'exact': 6 }, 'bounds': { 'lower': -6.4, 'upper': 6.35 } }
    strict = { 'size.exact': 6, 'bounds.lower': -6.4, 'bounds.upper': 6.35 }
    
    test_cfg = ('6',)
    
    def __init__(self, cfg=None, **other_cfg):
        super(FMS, self).__init__(cfg, **other_cfg)
        # build the standard y0 and index theta*t data points
        # - only 100 points in memory so what the heck :)
        self._v0 = (1.0, 5.0, -1.5, 4.8, 2.0, 4.9)
        self._theta = 2 * pi / 100 # for 100 sample points starting at i=1
        self._theta_t = [self._theta*i for i in xrange(0, 101)] # theta values
        self._y0 = [self._fms(self._v0, i) for i in self._theta_t ] # y0 values
    
    def _eval(self, indiv):
        '''f(x) = \sum\limit_t^100 (y(t)-y_0(t))^2
        '''
        total = 0.0 # summed squared error
        fms = self._fms
        
        # use the pre-computed theta*t values and reference y0[t] values
        #for theta_t, y0_t in izip(self._theta_t, self._y0):
        for t in xrange(101):
            theta_t = self._theta_t[t]
            y0_t = self._y0[t]
            total += (fms(indiv, theta_t) - y0_t)**2
        # easy done...
        return total
    
    
    def _fms(self, v, theta_t):
        '''y(t) = a1 * sin(w1*t*theta + a2 *
                           sin(w2*t*theta + a3 * sin(w3*t*theta)))
        '''
        return v[0] * sin(v[1]*theta_t + v[2]*sin(v[3]*theta_t + v[4]*sin(v[5]*theta_t)))

#=======================================================================
# Multi-objective Landscapes
#=======================================================================

class SCH(Real):
    '''Schaffer's simple two-objective benchmark problem.
    
    A simple multiobjective landscape given by Schaffer in "Multiple
    objective optimization with vector evaluated genetic algorithms"
    (1995)::
    
        f1(x) = x ^ 2
        f2(x) = (x - 2) ^ 2
    
    Optimal solutions are found in the interval [0, 2].
    '''
    lname = 'SCH'
    maximise = False
    default = { 'size': { 'exact': 1 }, 'bounds': { 'lower': -1000, 'upper': 1000 } }
    
    test_cfg = ('1','4')
    
    def eval(self, indiv):
        '''f1(x) = \sum\limit_i (x_i^2)
        f2(x) = \sum\limit_i ((x_i-2)^2)
        '''
        if self.invert:
            return SimpleDominatingFitness(2)([
                -sum(i**2 for i in indiv),
                -sum((i-2)**2 for i in indiv)
            ])
        else:
            return SimpleDominatingFitness(2)([
                sum(i**2 for i in indiv),
                sum((i-2)**2 for i in indiv)
            ])

class FON(Real):
    '''Fonseca and Fleming's two objective benchmark problem.
    
    A simple multiobjective landscape given by Fonseca and Fleming in
    "Multiobjective optimization and multiple constraint handling with
    evolutionary algorithms" (1998)::
    
        f1(x) = 1 - exp( -\sum\limit_i ((x_i - 1/\sqrt(3))^2) )
        f2(x) = 1 - exp( -\sum\limit_i ((x_i + 1/\sqrt(3))^2) )
    
    Optimal solutions are found in the interval [-1/sqrt(3), 1/sqrt(3)].
    '''
    lname = 'FON'
    maximise = False
    default = { 'size': { 'exact': 3 }, 'bounds': { 'lower': -4, 'upper': 4 } }
    
    test_cfg = ('3',)
    
    def eval(self, indiv):
        '''f1(x) = 1 - exp( -\sum\limit_i ((x_i - 1/\sqrt(3))^2) )
        f2(x) = 1 - exp( -\sum\limit_i ((x_i + 1/\sqrt(3))^2) )
        '''
        offset = 1 / sqrt(3)
        
        if self.invert:
            return SimpleDominatingFitness(2)([
                -sum((i-offset)**2 for i in indiv),
                -sum((i+offset)**2 for i in indiv)
            ])
        else:
            return SimpleDominatingFitness(2)([
                sum((i-offset)**2 for i in indiv),
                sum((i+offset)**2 for i in indiv)
            ])

