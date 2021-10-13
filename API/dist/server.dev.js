"use strict";

var express = require('express'); //const ml5 =  require('./ml5.min.js');


var app = express();
var PORT = 8080;
app.use(express.json());
app.post('/image', function (req, res) {
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
app.get('/image', function (req, res) {
  res.send({
    hello: 'hello'
  });
});
app.listen(PORT, function () {
  console.log("its alive on http://localhost:".concat(PORT)); //test = ml5.imageClassifier('DoodleNet');
});