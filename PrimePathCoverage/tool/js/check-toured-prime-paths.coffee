



# get user input values when form is submitted
formSubmitted = (evt) ->
  evt.preventDefault()
  $form = $(evt.currentTarget)

  # form values
  allowDetours = $("#allowDetours").is(':checked')
  primePaths = State.primePaths
  logs = $form.find("#logs").val()

  # transform values
  logs = splitNewline logs
  logs = _.compact _.map logs, trim

  # start checking if each prime path is toured
  isTouredMap = if allowDetours
  then checkDetourPrimePaths primePaths, logs
  else checkTouredPrimePaths primePaths, logs
  
  # show toured prime maths
  showTouredResults isTouredMap



# show results of toured prime paths
showTouredResults = (isTouredMap) ->
  $result = $("#result")

  showToured = (isToured, i) ->
    badge = if isToured then 'badge badge-success' else 'badge badge-inverse'
    ok = if isToured then 'icon-ok' else 'icon-exclamation-sign'
    color = if isToured then '#a5e032' else ''
    $label = $(document.createElement("span")).addClass(badge)
    $ok = $(document.createElement("span")).addClass(ok).css('margin-left': '5px', 'background-color': color)
    $path = $result.find "#prime-path-#{i}"
    $path.find('.path').html(isToured.join(', ')) if isToured.length
    $label.append $ok
    $path.append $ok

  _.each isTouredMap, showToured




# check all prime paths that are toured
checkTouredPrimePaths = (primePaths, logs) ->
  return _.map primePaths, (path) -> isSubPath path, logs


# check all prime paths that are toured with detours
checkDetourPrimePaths = (primePaths, logs) ->
  testPaths = splitArray logs, "END"

  checkDetours = (path, testPaths) ->
    detours = _.map testPaths, (testPath) -> (isDetoursPath path, testPath) and (whichDetoursPath path, testPath)
    detours = _.compact detours
    return false unless detours.length
    return _.min detours, (detour) -> detour.length

  isDetoursPath = (path, testPath) ->
    return true if path.length is 0
    index = _.indexOf testPath, _.first path
    rest = _.rest path
    restTestPath = testPath.slice index+1
    return index isnt -1 && isDetoursPath rest, restTestPath

  whichDetoursPath = (path, testPath, rec = false) ->
    return [] if path.length is 0
    first = _.first path
    index = _.indexOf testPath, first
    rest = _.rest path
    detourPart = _.map (testPath.slice 0, index), (node) -> "<em>#{node}</em>"
    detours = if rec then detourPart.concat [first] else [first]
    restTestPath =  testPath.slice index+1
    return detours.concat whichDetoursPath rest, restTestPath, true
    
  detours = _.map primePaths, (path) -> checkDetours path, testPaths
  return detours



# helper functies

splitArray = (arr, val) ->
  sep = ','
  j = arr.join sep
  s = j.split val
  _.map s, (elm) -> elm.split sep

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



# register event listeners on startup
$(document).ready ->
  $form = $("#form-check-toured-prime-paths")
  $form = $("#form-check-toured-prime-paths")
  $form.on 'submit', formSubmitted



