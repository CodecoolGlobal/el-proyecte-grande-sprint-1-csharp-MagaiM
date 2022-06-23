import React from 'react';
import Popup from 'reactjs-popup';

const InvalidLoginPopUp = () => {
    return (
        <Popup
            trigger={<button className="button"> Open Modal </button>}
            modal nested> {close =>
            (<div className="modal">
                <button className="close" onClick={close}> &times; </button>
                <div className="header"> Modal Title</div>
                <div className="content"> {' '} valami</div>
                <div className="actions">
                    <Popup trigger={<button className="button"> Trigger </button>}
                           position="top center" nested>
                        <span>valami</span>
                    </Popup>
                    <button className="button" onClick={() => {
                        console.log('modal closed ');
                        close();
                    }}> close modal
                    </button>
                </div>
        </div>)}
        </Popup>
    );

}

export default InvalidLoginPopUp