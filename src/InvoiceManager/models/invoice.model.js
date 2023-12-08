module.exports = (sequelize, Sequelize) => {
    const Invoice = sequelize.define("invoice", {
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
      }
    });
  
    return Invoice;
  };
  