import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import { Button } from 'reactstrap';
import PageHeader from '../../../shared/components/PageHeader/index';
import Profile from '../../../shared/components/Profile';

function Users() {
  const [alerts, setAlerts] = useState([]);
  const { userId } = useParams();
  console.log(userId);
  return (
    <div>
      <PageHeader title='User Setup' alerts={alerts}>
        <Button color='primary' form='profileForm'>
          Save Changes
        </Button>
      </PageHeader>
      <Profile mode='inspect' setAlerts={setAlerts} paramsId={userId} />
    </div>
  );
}

export default Users;
