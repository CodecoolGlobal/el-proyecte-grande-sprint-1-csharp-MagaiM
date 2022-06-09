import React, {useEffect, useState} from 'react';
import PropTypes from 'prop-types'

const Home = ({fetchData})  => {
        const [homeData, setHomeData] = useState([]);
        const [slideData, setSlideData] = useState([]);
        useEffect(() => {
            fetchData('https://localhost:7064/Home')
        .then(data => {
            data = data.slice(0, 15);
            setHomeData(data);
        })
    }, [])


       useEffect(() => {
        fetchData('https://localhost:7064/Deals?sortBy=Deal Rating&pageSize=20')
        .then(data => {
            data = data.slice(0, 15);
            setSlideData(data);
        })
    }, [])


    return (
            <div>
                <div className="slideshow-container" id="slideshow-container" style={{textAlignLast: 'center'}}>
                    {slideData.map((data, index) => (
                        <div key={index} className="mySlides fade" style={{ display: 'none'}}>
                            <img className="slide-show-image" src={data.Image} alt={'Image'}/>
                                <h5 className="card-title text-center">{data.Title}</h5>
                                <p className="card-text"><strong> Store name: {data.StoreName} <br></br> Sale price:
                                    ${String(data.SalePrice)} <br></br> Normal price: $ {String(data.NormalPrice)}</strong></p>
                        </div>
                    ))}

                </div>
                <br></br>
                <br></br>
                <div id='containerbody' style={{textAlign: 'center'}}>
                    {homeData.map((data, index) => (
                        <div key={index} className="col-md-auto col-md-auto bg-dark"
                             style={{display: 'inline-block', width: '300px', height: '475px', marginTop: '10px', marginBottom: '10px'}}>
                            <div className="card bg-dark" style={{boxShadow: '1px 2px 3px 4px rgba(112,128,144,0.4)', margin:'10px 15px 20px 20px',height: '465px'}}>
                                <a href={data.GameUrl}><img src={data.Thumbnail} alt={"Thumbnail"}
                                                            style={{width: '100%'}}/></a>
                                <div className="card-body inner-card" style={{backgroundColor: '#3A373F'}}>
                                    <h5 className="card-title text-center">{data.Title}</h5>
                                    <p className="card-text text-box">Short Description: {data.ShortDescription}</p>
                                    <p className="card-text">Genre: {data.Genre}</p>
                                    <p className="card-text">Platform: {data.Platform}</p>
                                    <p className="card-text text-center">
                                        <strong>ReleaseDate: {data.ReleaseDate}</strong></p>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        );
}
Home.prototype = {
    fetchData: PropTypes.func,
}


export default Home;