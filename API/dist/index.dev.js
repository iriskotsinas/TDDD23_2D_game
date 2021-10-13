"use strict";

var express = require('express');

var ml5 = require('ml5');

var app = express();
var PORT = 8080;
app.use(express.json());
app.post('/image/', function (req, res) {
  var b64_image = req.body.b64_image;

  if (!b64_image) {
    res.status(418).send({
      message: 'We need image'
    });
  }

  res.send({
    b64_image: "".concat(b64_image)
  });
});
app.listen(PORT, function () {
  console.log("its alive on http://localhost:".concat(PORT));
  doodleClassifier = ml5.imageClassifier('DoodleNet');
});