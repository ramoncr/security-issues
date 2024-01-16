const invoiceModel = require("./invoice.model");

module.exports = (sequelize, Sequelize) => {
    const Client = sequelize.define("client", {
      number: {
        type: Sequelize.STRING
      },
      title: {
        type: Sequelize.STRING
      },
      description: {
        type: Sequelize.STRING
      }
    });


    return Client;
  };
  