const clientModel = require("./client.model");

module.exports = (sequelize, Sequelize) => {
    const Invoice = sequelize.define("invoice", {
      number: {
        type: Sequelize.STRING
      },
      title: {
        type: Sequelize.STRING
      },
      description: {
        type: Sequelize.STRING
      },
      published: {
        type: Sequelize.BOOLEAN
      },
      publishedDate: {
        type: Sequelize.STRING
      },
      dueDate: {
        type: Sequelize.STRING
      },
      amount: {
        type: Sequelize.FLOAT
      },
    });

    return Invoice;
  };
  