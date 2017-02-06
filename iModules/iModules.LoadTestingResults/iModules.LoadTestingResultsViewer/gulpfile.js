/// <binding BeforeBuild='clean, default' Clean='clean' />
var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    del = require("del"),
    less = require("gulp-less"),
    runSequence = require("run-sequence"),
    sourcemaps = require("gulp-sourcemaps");

var webroot = "wwwroot/";
var paths = {
    js: webroot + "js/**/*.js",
    minJs: webroot + "js/**/*.min.js",
    css: webroot + "css/**/*.css",
    minCss: webroot + "css/**/*.min.css",
    concatJsDest: webroot + "js/site.min.js",
    concatCssDest: webroot + "css/site.min.css",
    less: "styles/**/*.less"
};

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(sourcemaps.init())
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(sourcemaps.write("."))
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

gulp.task("clean:js", function (cb) {
    del(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    del(paths.css, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("less", function (cb) {
    return gulp.src(paths.less)
        .pipe(less())
        .pipe(gulp.dest(webroot + "/css"));
});

gulp.task("default", function (cb) {
    runSequence(
        "less",
        "min",
        cb);
});