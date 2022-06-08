import React, { Component, useEffect } from 'react';
import { useState } from "react";
import Container from './components/Container';
import Header from './components/Header';
import News from './components/News';

function App(){

    const [text , setText] = useState("")
    const [data, setData] = useState([]);

    useEffect(() => {
        fetchData('https://localhost:7064/News')
        .then(data => {
            setData(data);
        })
    }, [])
   
    const loadNews = () => {
        setText(<News fetchData={fetchData} showGameNews={showGameNews} />)
    }

    const showGameNews = (event) => {
        console.log("ShowGameNewsEvent");
    };

    return (
        <div className='bg-dark text-white'>
            <Header loadNews={loadNews} />
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

export default App;