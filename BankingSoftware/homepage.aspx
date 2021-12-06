<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="BankingSoftware.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="customCss/homepage.css" rel="stylesheet" />
    <center>
        <div class="slideshow-container">
            <div class="mySlides fade">
                <img src="imgs/img1.jpg"  height="454" class="centered">
            </div>
            <div class="mySlides fade">
                <img src="imgs/img2.jpg" height="454">
            </div>
            <div class="mySlides fade">
                <img src="imgs/img3.jpg"  height="454">
            </div>
        </div>
        <br>
        <div style="text-align:center">
            <span class="dot"></span> 
            <span class="dot"></span> 
            <span class="dot"></span> 
        </div>
    </center>
    <script>
        var slideIndex = 0;
        showSlides();

        function showSlides() {
            var i;
            var slides = document.getElementsByClassName("mySlides");
            var dots = document.getElementsByClassName("dot");

            for (i = 0; i < slides.length; i++)
            {
                slides[i].style.display = "none";
            }

            slideIndex++;

            if (slideIndex > slides.length) { slideIndex = 1 }

            for (i = 0; i < dots.length; i++)
            {
                dots[i].className = dots[i].className.replace(" activeB", "");
            }

            slides[slideIndex - 1].style.display = "block";
            dots[slideIndex - 1].className += " activeB";
            setTimeout(showSlides, 2000);
        }
    </script>
</asp:Content>