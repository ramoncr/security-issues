const express = require("express");
const router = express.Router();
const db = require("../models");
const passport = require("passport");
var LocalStrategy = require("passport-local");
var crypto = require('crypto');

passport.use(
  new LocalStrategy(function verify(username, password, cb) {
    db.users
      .findAll({
        where: {
          username: username,
        },
      })
      .then((result) => {
        console.log('Authentication attempt: ' + username + password);
        if (result.length !== 1) {
          return cb(null, false, {
            message: "Incorrect username or password.",
          });
        }
        const user = result[0];
        
        crypto.pbkdf2(
          password,
          user.salt,
          310000,
          32,
          "sha256",
          function (err, hashedPassword) {
            if (err) {
              return cb(err);
            }
            if (!crypto.timingSafeEqual(user.password, hashedPassword)) {
              return cb(null, false, {
                message: "Incorrect username or password.",
              });
            }
            return cb(null, user);
          }
        );
      })
      .catch((err) => {
        return cb(err);
      });
  })
);

passport.serializeUser(function(user, cb) {
  process.nextTick(function() {
    cb(null, { id: user.id, username: user.username });
  });
});

passport.deserializeUser(function(user, cb) {
  process.nextTick(function() {
    return cb(null, user);
  });
});



router.get("/login", function (req, res, next) {
  res.render("login");
});

router.post(
  "/login/password",
  passport.authenticate("local", {
    successRedirect: "/",
    failureRedirect: "/login",
  })
);

router.get('/logout', function(req, res, next) {
  req.logout(function(err) {
    if (err) { return next(err); }
    res.redirect('/');
  });
});

router.get('/signup', function(req, res, next) {
  res.render('signup');
});

router.post('/signup', function(req, res, next) {
  var salt = crypto.randomBytes(16);
  crypto.pbkdf2(req.body.password, salt, 310000, 32, 'sha256', function(err, hashedPassword) {
    if (err) { return next(err); }

    db.users.create({
      username: req.body.username,
      password: hashedPassword,
      salt: salt
    }).then(result => {
      console.log(result);
      res.redirect('/');
    })
  });
});


module.exports = router;