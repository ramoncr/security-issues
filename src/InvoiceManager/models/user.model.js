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
        type: Sequelize.STRING
      },
      salt: {
        type: Sequelize.STRING
      },
      strategy: {
        type: Sequelize.STRING,
      },
      locked: {
        type: Sequelize.BOOLEAN
      },
      isAdmin : {
        type: Sequelize.BOOLEAN
      }
    });

    return User;
  };
  