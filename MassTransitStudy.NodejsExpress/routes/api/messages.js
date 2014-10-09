var SampleMessage = require('../../models/samplemessage.js');
var Uuid = require('node-uuid');


exports.list = function(req, res) {
    SampleMessage.find(function(error, messages) {
        if (error) return res.sendStatus(500);

        res.json(messages);
    });
};

exports.add = function(req, res) {
    if (!req.body) return res.sendStatus(400);

    var sampleMessage = new SampleMessage();

    sampleMessage.Id = Uuid.v4();
    sampleMessage.Data = req.body.Data;
    sampleMessage.Timestamp = req.body.Timestamp;

    sampleMessage.save(
        function(error) {
            if (error) return res.sendStatus(500);

            res.json(sampleMessage);
        });
};