const express = require("express");
const router = express.Router();
const db = require("../models");

/* GET users listing. */
router.get("/:invoiceNumber", function (req, res, next) {
  const invoiceNumber = req.params.invoiceNumber;

  db.invoices
    .findByPk(invoiceNumber)
    .then((data) => {
      if (data) {
        res.render("invoice", data);
      } else {
        res.render("invoice", null);
      }
    })
    .catch((err) => {
      res.render("invoice", null);
    });
});

/**
 * Example request body:
 * {
 *   condition: { title: 'Test*' }
 * }
 */

router.post("/filter", function (req, res, next) {
  const condition = req.body.condition;
  db.invoices
    .findAll({
      where: condition,
    })
    .then((data) => {
      if (data) {
        res.render("invoices", { invoices: data });
      } else {
        res.render("invoices", { invoices: [] });
      }
    })
    .catch((err) => {
      res.render("invoices", { invoices: [] });
    });
});

router.post("/search", async function (req, res, next) {
  const searchText = req.body.searchText;

  const [result, metadata] = await sequelize.query(
    `SELECT * FROM invoices WHERE content LIKE '${searchText}'`
  );

  if (result) {
    res.render("invoice", result);
  } else {
    res.render("invoice", null);
  }
});


router.post("/", async function(req, res, next) {
  const model = req.body;

  if(!model.name || !model.amount) {
    res.status(400);
    res.send({
      message: "Invalid model request",
    })
  } 

  const combinedData = Object.assign(
    {
      ownerId: User.GetUserId(),
    },
    model
  );

  var instance = Invoice.Build(combinedData);
  await instance.save();
});
module.exports = router;
