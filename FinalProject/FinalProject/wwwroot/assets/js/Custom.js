$(document).ready(function () {
    $(document).on("click", ".addtocart", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        console.log(url)
        fetch(url).then(res => {
            return res.text()
            console.log(url)
        }).then(data => {
            $(".minicart-inner").html(data);
            fetch("home/count/").then(res => {
                return res.text()
            }).then(data => {
                $(".notification").html(data)
            })
        })
    })
    $(document).on("click", "#addbasketbtn", function (e) {
        e.preventDefault()

        let url = $("#basketform").attr("action")
        let count = $("#productcount").val();

        var e = document.getElementById("colorids");
        var colorID = e.options[e.selectedIndex].value;

        var e = document.getElementById("sizeids");
        var sizeID = e.options[e.selectedIndex].value;

        console.log(colorID)
        console.log(sizeID)

        url = url + "?count=" + count + "&colorid=" + colorID + "&sizeid=" + sizeID;
        fetch(url).then(response => {
            return response.text();
        }).then(data => {
            $(".minicart-inner").html(data)
            fetch("home/count/").then(res => {
                return res.text()
            }).then(data => {
                $(".notification").html(data)
            })
        })
    })
    $(document).on("click", ".deletecard", function (e) {
        e.preventDefault();

        let url = $(this).attr("href");

        fetch(url).then(response => {
            fetch("Basket/GetBasket").then(response => response.text()).then(data => $(".minicart-inner").html(data))


            fetch("home/count").then(res => {
                return res.text()
            }).then(data => {
                $(".notification").html(data)
            })
            return response.text()
        })
            .then(data => {
            $(".basketContainer").html(data);
        })


    })
    $(document).on("click", ".deletebasket", function (e) {
        e.preventDefault();

        let url = $(this).attr("href");

        fetch(url).then(response => {
            fetch("Basket/GetCard").then(response => response.text()).then(data => $(".basketContainer").html(data))

            fetch("home/count").then(res => {
                return res.text()
            }).then(data => {
                $(".notification").html(data)
            })
            return response.text()
        }).then(data => $(".minicart-inner").html(data))
    })
    $(document).on("click",".dec", function (e) {

        e.preventDefault();
        console.log("salam");

        var id = $(this).next().attr("data-id");
        var color = parseInt($(this).next().attr("data-color"));
        var size = parseInt($(this).next().attr("data-size"));

        var productCount = parseInt($(this).next().attr("data-productCount"));

        let prodCount = parseInt($(this).next(".prod-count").val());

        if (prodCount <= 1) {
            prodCount = 1
        }
        else if (prodCount <= productCount) {
            prodCount--;
        }
        else {
            prodCount = productCount;
        }

        var count = prodCount;

        fetch("../../Basket/Update" + "?id=" + id + "&count=" + count + "&color=" + color + "&size=" + size).then(response => {

            fetch("../../Basket/GetBasket").then(response => response.text())
                .then(data => $(".minicart-inner").html(data))
            return response.text()

        }).then(data => $(".basketContainer").html(data))



        $(this).next(".prod-count").val(prodCount);
    })
    $(document).on("click", ".inc", function (e) {

        e.preventDefault();
        console.log("salam");

        var id = $(this).prev().attr("data-id");
        var color = parseInt($(this).prev().attr("data-color"));
        var size = parseInt($(this).prev().attr("data-size"));

        var productCount = parseInt($(this).prev().attr("data-productCount"));


        let prodCount = parseInt($(this).prev(".prod-count").val());
        if (prodCount <= 1) {
            prodCount = 1
        }
        else if (prodCount < productCount) {
            prodCount++;
        } else {
            prodCount = productCount;
        }
        var count = prodCount;
        fetch("../../Basket/Update" + "?id=" + id + "&count=" + count + "&color=" + color + "&size=" + size).then(response => {

            fetch("../../Basket/GetBasket").then(response => response.text())
                .then(data => $(".minicart-inner").html(data))
            return response.text()

        }).then(data => $(".basketContainer").html(data))

        $(this).prev(".prod-count").val(prodCount);
    })
    $(document).on("keyup", ".prod-count", function () {
        var productCount = parseInt($(this).attr("data-productCount"));
        var count = 1;
        if (parseInt($(this).val()) <= parseInt(productCount)) {
            count = parseInt($(this).val());
        }
        else {
            count = parseInt(productCount);
        }

        var id = $(this).attr("data-id");
        var color = parseInt($(this).attr("data-color"));
        var size = parseInt($(this).attr("data-size"));


        fetch("../../Basket/Update" + "?id=" + id + "&count=" + count + "&color=" + color + "&size=" + size).then(response => {

            fetch("../../Basket/GetBasket").then(response => response.text())
                .then(data => $(".minicart-inner").html(data))
            return response.text()

        }).then(data => $(".basketContainer").html(data))

        $(this).val(count);;

    })
    
    $(document).on("click", ".default-link", function (e) {
        e.preventDefault();
    })

    $(document).on("keyup", "#searchBtn", function (e) {
        e.preventDefault()
        console.log($(this).val())
        let url = $("#searchForm").attr("action");
       
        fetch(url + "?key=" + $(this).val()).then(res => {
            return res.text()
        }).then(data => {
            $("#productList").html(data)
        })
    })
})