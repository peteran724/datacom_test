import { useState } from 'react';
import { addApplication } from '../services/api';

const AddApplication = ({ onAdd }) => {

    const [company, setCompany] = useState('');
    const [position, setPosition] = useState('');

    const handleSubmit = async () => {
        await addApplication({ companyName: company, position: position });
        onAdd();
        setCompany('');
        setPosition('');
    };

    return (
        <div className="add-application-div">
            <div className="form-group">
                <label htmlFor="company">Company Name:</label>
                <input
                    type="text"
                    id="company"
                    value={company}
                    onChange={(e) => setCompany(e.target.value)}
                    required
                    placeholder="Enter company name"
                />
            </div>

            <div className="form-group">
                <label htmlFor="position">Position:</label>
                <input
                    type="text"
                    id="position"
                    value={position}
                    onChange={(e) => setPosition(e.target.value)}
                    required
                    placeholder="Enter position"
                />
            </div>

            <button className="submit-button" onClick={handleSubmit}>
                Add Application
            </button>
        </div>
    );
};


export default AddApplication