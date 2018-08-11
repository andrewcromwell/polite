/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var concat = require('gulp-concat');
var cleanCSS = require('gulp-clean-css');
var rimraf = require('gulp-rimraf');
var less = require('gulp-less');

var paths = {
    lessFiles: "./Content/less/*.less",
    mincss: "./Content/css/*.min.css"
}

gulp.task("clean:css", function (cb) {
    rimraf(paths.mincss, cb);
});

gulp.task('min:css', function () {
    return gulp.src([paths.lessFiles])
        .pipe(less())
        .pipe(concat('blueish.min.css'))
        .pipe(cleanCSS({ compatibility: 'ie8' }))
        .pipe(gulp.dest("./Content/css"));
})

gulp.task('default', ["clean:css", "min:css"]);