$(document).ready(function () {
    var courseCount = $("#courseCount").val();
    var blogCount = $("#blogCount").val();

    var skipcourse = 3;
    var skipblog = 3;

    $(document).on("click", "#loadMore", function () {

        $.ajax({
            url: "/Home/LoadCourses?skip=" + skipcourse,

            type: "GET",

            success: function (response) {

                $("#courseRow").append(response)

                skipcourse += 3
                console.log(skipcourse, courseCount, $("#courseRow"));

                if (skipcourse >= courseCount) {
                    $("#loadMore").remove();
                }
            },

            error: function (xhr) {

            }
        });

    });


    $(document).on("click", "#loadMore2", function () {

        $.ajax({
            url: "/Home/LoadBlogs?skip=" + skipblog,
            type: "GET",

            success: function (response) {

                $("#blogRow").append(response)

                skipblog += 3

                if (skipblog >= blogCount) {
                    $("#loadMore2").remove();
                }
            },

            error: function (xhr) {

            }
        });
    });

    $(document).on('keyup', '#searchedCourses', function () {

        var searchedCoursesTitle = $(this).val()

        $.ajax({
            url: `/Course/Search?searchedCoursesTitle=${searchedCoursesTitle}`,
            type: "GET",

            success: function (response) {
                $('#searched_Course').slice(1)
                $('#searched_Course').append(response)
            },

            error: function (xhr) {

            }
        })
    })
});


