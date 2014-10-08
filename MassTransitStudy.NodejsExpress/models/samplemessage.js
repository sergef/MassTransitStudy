var mongoose = require('mongoose');

var Schema = mongoose.Schema;

var SampleMessageSchema = new Schema(
{
    Id: String,
    Data: String,
    Timestamp: Date
});

module.exports = mongoose.model(
    'SampleMessage',
    SampleMessageSchema);