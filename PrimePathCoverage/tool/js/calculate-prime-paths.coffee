
# store the prime paths results globally so it can be used
# by other modules, eg. toured prime path checker
@State = 
  primePaths: null



# show results of calculated prime paths
State.showResults = (primePaths) ->
  $result = $("#result")

  showPrimePath = (path, i) ->  
    $div = $(document.createElement("div")).attr('id', "prime-path-#{i}")
    $label = $(document.createElement("span")).addClass('name').text("path #{i+1}: ")
    $nodes = $(document.createElement("span")).addClass('path').text(path.join(', '))
    $div.append $label, $nodes
    
  $paths = _.map primePaths, showPrimePath
  $result.html $paths




# get user input values when form is submitted
formSubmitted = (evt) ->
  evt.preventDefault()
  $form = $(evt.currentTarget)

  # form values
  edges = $form.find("#edges").val()
  
  # transform values
  edges = splitNewline edges
  edges = _.compact _.map edges, trim
  edges = _.map edges, strToEdge

  # determine nodes
  nodes = _.uniq _.flatten edges

  # start calculating prime paths
  primePaths = calculatePrimePaths [nodes, edges, null, null]
  
  # store results
  State.primePaths = primePaths

  # show prime maths
  State.showResults primePaths








# calculation all prime path for the given graph
calculatePrimePaths = (graph) ->
  [nodes, edges, startNode, endNode] = graph

  # init state
  paths = _.map nodes, toList
  endedPaths = []
  cyclePaths = []

  iterate = ->

    transitionEdges = (path) ->
      last = _.last path
      transEdges = _.filter edges, ([from,to]) -> from is last
      return transEdges

    expand = (path) ->
      transEdges = transitionEdges path
      transPaths = _.map transEdges, ([from,to]) -> [].concat path, [to]
      return transPaths

    isCycle = (path) ->
      last = _.last path
      first = _.first path
      return path.length > 1 and last is first

    isEnded = (path) ->
      transEdges = transitionEdges path
      return not transEdges.length

    isSimple = (path) ->
      last = _.last path
      return not (last in _.initial path)

    removeCyclePaths = (paths, cyclePaths) ->
      cyclePaths = _.union cyclePaths, _.filter paths, isCycle
      paths = _.reject paths, isCycle
      return [paths, cyclePaths]

    removeNotSimplePaths = (paths, endedPaths) ->
      notSimplePaths = _.reject paths, isSimple
      paths = _.filter paths, isSimple
      endedPaths = _.union endedPaths, _.map notSimplePaths, _.initial
      return [paths, endedPaths]

    removeEndedPaths = (paths, endedPaths) ->
      endedPaths = _.union endedPaths, _.filter paths, isEnded
      paths = _.reject paths, isEnded
      return [paths, endedPaths]


    # expand paths by following the possible edges
    paths = _.flatten (_.map paths, expand), true 

    # remove the cycle paths
    [paths, cyclePaths] = removeCyclePaths paths, cyclePaths

    # remove paths that are not simple
    [paths, endedPaths] = removeNotSimplePaths paths, endedPaths

    # remove paths that can't be expanded
    [paths, endedPaths] = removeEndedPaths paths, endedPaths

    
    # some paths needs to be further expanded    
    iterate() if paths.length


  # start iterating to find all simple paths of maximum length
  iterate()

  # remove simple paths that are a subpath of another 
  # simple path. Notice that cyclePaths can never be a subpath 
  # of an other simple path
  allPaths = _.union endedPaths, cyclePaths  
  endedPaths = _.reject endedPaths, (subpath) -> 
    otherPaths = _.without allPaths, subpath
    return _.some otherPaths, _.partial isSubPath, subpath

  # combining the ended paths and the cycle paths give us all prime paths
  primePaths = _.union endedPaths, cyclePaths

  console.log  'primePaths', primePaths

  return primePaths
  
  








# helper functions

splitNewline = (str) ->
  str.split /\r\n|\r|\n/g

trim = (str) ->
  str.replace /^\s+|\s+$/g, ''

strToEdge = (str) ->
  str.split /\s+/

isSubPath = (sub, path) ->
  return true if sub.length is 0
  return false if path.length < sub.length
  return (isInitialEqual sub, path) or (isSubPath sub, path.slice 1)

isInitialEqual = (path1, path2) ->
    length = Math.min path1.length, path2.length
    path1 = path1.slice 0, length
    path2 = path2.slice 0, length
    return isEqual path1, path2

isEqual = (path1, path2) ->
  path = _.zip path1, path2
  return _.every path, ([n1,n2]) -> n1 is n2

toList = (elm) ->
  return [elm]



# register event listeners on startup
$(document).ready ->
  $form = $("#form-calculate-prime-paths")
  $form.on 'submit', formSubmitted
  
  # for testing
  formSubmitted 
    preventDefault: ->
    currentTarget: $form



