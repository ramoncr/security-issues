const dbConfig = require("../config/db.config");
const Sequelize = require("sequelize");

const sequelize = new Sequelize({
    dialect: 'sqlite',
    storage: 'db.sqlite'
  });

  const db = {};
  db.Sequelize = Sequelize;
  db.sequelize = sequelize;

  db.invoices = require('./invoice.model')(sequelize, Sequelize);

  module.exports = db;