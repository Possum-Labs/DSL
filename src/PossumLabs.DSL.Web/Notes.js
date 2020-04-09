/// not part of the solution, using this to get syntax highlighting.

var xpath = "//*[text()='Login']";
var elements = document.evaluate(xpath, document, null, XPathResult.ANY_TYPE, null); 
var thisElement = elements.iterateNext(); 
var index = 0; 
var count = 0;
var lastFind = -1;
var debugging = "elements are:\n";
var elementsList = [];

while (thisElement) {
    elementsList.push(thisElement);
    var rect = thisElement.getBoundingClientRect();
    var pointElement = document.elementFromPoint(rect.x + 1, rect.y + 1);
    var clickable = thisElement === pointElement || thisElement.contains(pointElement);
    if (clickable) {
        count++;
        lastFind = index;
    }
    debugging += "element:"+thisElement.tagName + 
        "\n" + "element from point:" + pointElement.tagName +
        "\n" + " clickable at point? " + clickable + 
        "\n";
    thisElement = elements.iterateNext();
    index++;
}
if (count === 1)
    elementsList[lastFind];
else
    debugging;