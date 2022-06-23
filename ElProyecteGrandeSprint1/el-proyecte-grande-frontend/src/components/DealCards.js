import React from 'react';
import { useState, useEffect } from "react";
import { useRef } from 'react';
import PropTypes from 'prop-types';

const DealCards = ({dealsData}) => {
    const scrl = useRef(null);
    const [scrollX, setscrollX] = useState(0);
    const [scrolEnd, setscrolEnd] = useState(false);

    const slide = (shift) => {
        scrl.current.scrollLeft += shift;
        setscrollX(scrollX + shift); 
        if (
          Math.floor(scrl.current.scrollWidth - scrl.current.scrollLeft) <=
          scrl.current.offsetWidth
        ) {
          setscrolEnd(true);
        } else {
          setscrolEnd(false);
        }
    };
    
    const scrollCheck = () => {
    setscrollX(scrl.current.scrollLeft);
    if (
        Math.floor(scrl.current.scrollWidth - scrl.current.scrollLeft) <=
        scrl.current.offsetWidth
    ) {
        setscrolEnd(true);
    } else {
        setscrolEnd(false);
    }
    };
  


  return (
    <div className='scrollable-deals-container'>
        {scrollX !== 0 && (
        <button className="scroll-button" onClick={() => slide(-450)}>
          <i className="fa fa-angle-left"></i>
        </button>
        )}
        <div className="scrolling-wrapper" style={{width: '100%'}} ref={scrl} onScroll={scrollCheck}>
                    {dealsData.map((deals) => (
                        <div key={deals.Title} className="col-md-auto col-md-auto bg-dark" style={{display: 'inline-block', width: '220px', height: '350px'}}>
                            <div className="deal-card" style={{boxShadow: '1px 2px 3px 4px rgba(112,128,144,0.4)', margin:'10px 10px 50px 10px'}}>
                                <div className='image-div'>
                                  <img className="product-image" src={String(deals.Image)}/>
                                </div>
                                <div className="card-body inner-card" style={{backgroundColor: '#3A373F', height:'175px'}}>
                                    <p className=" deal-title">{deals.Title}</p>
                                        <p className="card-text deal-info">Deal rating: {deals.DealRating}({Math.floor(100-(deals.SalePrice/deals.NormalPrice)*100)}%)</p>
                                        <p className="card-text price-info">Sale price: ${String(deals.SalePrice)}</p>
                                        <p className="card-text price-info">Normal price: ${String(deals.NormalPrice)}</p>
                                </div>
                            </div>
                            <br/>
                        </div>
                       
                    ))}
                </div>
                {!scrolEnd && (
        <button className="scroll-button" onClick={() => slide(+450)}>
          <i className="fa fa-angle-right"></i>
        </button>
        )}
        </div>
  )
}


export default DealCards