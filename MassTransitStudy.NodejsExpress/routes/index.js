
/*
 * GET home page.
 */

exports.index = function (req, res) {
    res.render('index', { title: 'MassTransitStudy Node.js + Express server' });
};