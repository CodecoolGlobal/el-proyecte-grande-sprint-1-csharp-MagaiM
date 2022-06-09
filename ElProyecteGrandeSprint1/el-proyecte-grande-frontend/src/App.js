import React, { Component, useEffect, useCallback } from 'react';
import { useState } from "react";
import Container from './components/Container';
import Header from './components/Header';
import News from './components/News';
import Deals from './components/Deals';
import Home from "./components/Home";
import {wait} from "@testing-library/user-event/dist/utils";

let slideIndex = 0;
function App(){
    const [text , setText] = useState("")
    const [data, setData] = useState([]);

    useEffect(() => {
        fetchData('https://localhost:7064/News/wtf').then(fetchedData => {
            setData(fetchedData)
        })
    }, [])

    const loadNews = useCallback( () => {
        fetchData(`https://localhost:7064/News/`)
            .then(data => {
            setText(<News fetchData={data} showGameNews={showGameNews} showLatestNews={loadNews} searchedNews={""} />)
        })
    }, [])

    const loadDeals = () => {
        setText(<Deals fetchData={fetchData} showDeals={showGameNews} />)
    }

    const loadHome = () => {
        setText(<Home fetchData={fetchData}/>)
        wait(1000).then(x => showSlides())

    }

    const showGameNews = useCallback((event) => {
        const searchedNews = event.target.textContent;
        fetchData(`https://localhost:7064/News/${searchedNews}`)
            .then(data => {
                setText(<News fetchData={data} showGameNews={showGameNews} showLatestNews={loadNews} searchedNews={searchedNews} />)
            })
    }, [])

    return (
        <div className='bg-dark text-white'>
            <Header loadNews={loadNews} loadDeals={loadDeals} loadHome={loadHome}/>
            <Container text={text} />
        </div>
    );
}

async function fetchData(url) {
    const response = await fetch(url);
    if (response.ok){
        const data = await response.json();
        return data;
    }
    throw response;
}


function showSlides() {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }
    slides[slideIndex - 1].style.display = "block";
    setTimeout(showSlides, 5000); // Change image every 2 seconds
}


export default App;