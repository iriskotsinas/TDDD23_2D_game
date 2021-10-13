"use strict";

var clearButton;
var canvas;
var doodleClassifier;
var resultsDiv;
var testImage = document.getElementById("airplane");

function setup() {
  canvas = createCanvas(400, 400);
  clearButton = createButton('clear');
  clearButton.mousePressed(clearCanvas);
  background(255);
  doodleClassifier = ml5.imageClassifier('DoodleNet', modelReady);
  resultsDiv = createDiv('model loading');
}

function modelReady() {
  console.log('model loaded');
  console.log(testImage);
  doodleClassifier.classify(testImage, gotResults);
} //Recursive function


function gotResults(error, results) {
  if (error) {
    console.error(error);
    return;
  } // console.log(results);


  var content = "".concat(results[0].label, " \n                 ").concat(nf(100 * results[0].confidence, 2, 1), "%<br/>\n                 ").concat(results[1].label, " \n                 ").concat(nf(100 * results[1].confidence, 2, 1), "%");
  return content; // resultsDiv.html(content);
  //doodleClassifier.classify(testImage, gotResults);
}

function clearCanvas() {
  background(255);
}

function draw() {
  if (mouseIsPressed) {
    strokeWeight(16);
    line(mouseX, mouseY, pmouseX, pmouseY);
  }
}

function classDrawing(image) {
  return doodleClassifier.classify(image, gotResults);
} // add the code below


module.exports = {
  classDrawing: classDrawing
};