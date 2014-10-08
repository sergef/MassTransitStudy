var SampleMessage = require('../../models/samplemessage.js');
var Uuid = require('node-uuid');


exports.list = function (req, res) {
    res.json(
    {
    });
};

exports.add = function (req, res) {
    if (!req.body) {
        return res.sendStatus(400);
    }

    var sampleMessage = new SampleMessage();

    sampleMessage.Id = Uuid.v4();
    sampleMessage.Data = req.body.Data;
    sampleMessage.Timestamp = req.body.Timestamp;

    sampleMessage.save();

    res.json(sampleMessage);
}