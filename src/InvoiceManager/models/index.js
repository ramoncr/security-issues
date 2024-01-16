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
  db.clients = require('./client.model')(sequelize, Sequelize);

  db.invoices.belongsTo(db.clients);
  db.clients.hasMany(db.invoices);


  module.exports = db;