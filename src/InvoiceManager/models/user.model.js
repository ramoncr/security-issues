const clientModel = require("./client.model");

module.exports = (sequelize, Sequelize) => {
    const User = sequelize.define("user", {
      username: {
        type: Sequelize.STRING
      },
      email: {
        type: Sequelize.STRING
      },
      password: {
        type: Sequelize.BLOB
      },
      salt: {
        type: Sequelize.BLOB
      },
      locked: {
        type: Sequelize.BOOLEAN
      }
    });

    return User;
  };
  