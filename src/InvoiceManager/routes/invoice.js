const express = require('express');
const router = express.Router();
const db = require('../models');

/* GET users listing. */
router.get('/:invoiceNumber', function(req, res, next) {
  const invoiceNumber = req.params.invoiceNumber;

  db.invoices.findByPk(invoiceNumber).then(data => {
    if(data) {
      res.render('invoice', data);
    } else {
      res.render('invoice', null);
    }
  }).catch(err => {
    res.render('invoice', null);
  });
});


/**
 * Example request body:
 * {
 *   condition: { title: 'Test*' }
 * }
 */

router.post('/filter', function(req, res, next) {
  const condition = req.body.condition;

  db.invoices.findAll({
    where: condition
  }).then(data => {
    if(data) {
      res.render('invoices', { invoices: data });
    } else {
      res.render('invoices', { invoices: [] });
    }
  }).catch(err => {
    res.render('invoices', { invoices: [] });
  });
});


// router.post('/by-user', function(req, res, next) {
//   const invoiceNumber = req.params.invoiceNumber;

//   db.invoices.where(invoiceNumber).then(data => {
//     if(data) {
//       res.render('invoice', data);
//     } else {
//       res.render('invoice', null);
//     }
//   }).catch(err => {
//     res.render('invoice', null);
//   });
// });

module.exports = router;
