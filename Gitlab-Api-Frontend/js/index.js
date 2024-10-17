fetch('https://localhost:7242/api/Group/groups')
.then(response => {
  if (!response.ok) {
    throw new Error('Response Error');
  }
  return response.json();
})
.then(data => {
  const tableBody = document.getElementById('group-table-body');
  data.forEach(group => {
      const row = `<tr>
      <td>${group.id}</td>
      <td>${group.name}</td>
      <td>${group.description || 'Yok'}</td>
      <td>${group.visibility}</td>
      <td><a href="projects.html?groupId=${group.id}" class="btn btn-primary">Projeler</a></td>
      </tr>`;

    tableBody.innerHTML += row;
  });
})
.catch(error => {
  console.error('API’den veri alırken hata oluştu:', error);
  const tableBody = document.getElementById('group-table-body');
  tableBody.innerHTML = `<tr><td colspan="6" class="text-center">Veri alınamadı.</td></tr>`;
});


