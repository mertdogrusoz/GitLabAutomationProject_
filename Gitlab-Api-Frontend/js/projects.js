const urlParams = new URLSearchParams(window.location.search);
const groupId = urlParams.get('groupId');

fetch(`https://localhost:7242/api/Group/groups/${groupId}/projects`)
  .then(response => {
    if (!response.ok) {
      throw new Error('Response Error');
    }
    return response.json();
  })
  .then(data => {
    const tableBody = document.getElementById('project-table-body');
    data.forEach(project => {
      const row = `<tr>
        <td>${project.id}</td>
        <td>${project.name}</td>
        <td>${project.description || 'Yok'}</td>
        <td><a href="${project.web_url}" target="_blank">${project.web_url}</a></td>
      </tr>`;
      tableBody.innerHTML += row;
    });
  })
  .catch(error => {
    console.error('API’den veri alırken hata oluştu:', error);
    const tableBody = document.getElementById('project-table-body');
    tableBody.innerHTML = `<tr><td colspan="4" class="text-center">Veri alınamadı.</td></tr>`;
  });

function fetchPackages(searchTerm = '') {
  fetch(`https://localhost:7242/api/Group/groups/${groupId}/projects/packages?searchTerm=${searchTerm}`)
    .then(response => {
      if (!response.ok) {
        throw new Error('Response Error');
      }
      return response.json();
    })
    .then(data => {
      const tableBody = document.getElementById('project-package-body');
      tableBody.innerHTML = ''; 
      if (data.length === 0) {
        tableBody.innerHTML = `<tr><td colspan="4" class="text-center">Hiç paket bulunamadı.</td></tr>`;
        return;
      }
      data.forEach(package => {
        const row = `<tr>
          <td>${package.projectName}</td>
          <td>${package.packageId}</td>
          <td>${package.version}</td>
          <td>
            <div class="form-check">
              <input class="form-check-input" type="checkbox" id="checkbox-${package.packageId}" onchange="updateSelectedProjects()">
              <label class="form-check-label" for="checkbox-${package.packageId}">
                Versiyon Güncellemesi
              </label>
            </div>
          </td>
        </tr>`;
        tableBody.innerHTML += row;
      });
    })
    .catch(error => {
      console.error('Paket verileri alınırken hata oluştu:', error);
      const tableBody = document.getElementById('project-package-body');
      tableBody.innerHTML = `<tr><td colspan="4" class="text-center">Veri alınamadı.</td></tr>`;
    });
}

fetchPackages();

document.getElementById('searchButton').addEventListener('click', () => {
  const searchTerm = document.getElementById('searchTerm').value;
  fetchPackages(searchTerm);
});

async function updatePackage(packageId, projectName, version) {
  const response = await fetch(`https://localhost:7242/api/Group/update-package-version?projectName=${projectName}&packageId=${packageId}&version=${version}`, {
    method: 'PUT'
  });
  
  if (!response.ok) {
    throw new Error(`Güncelleme hatası: ${response.status}`);
  }
  return response.text();
}

document.getElementById('update-button').addEventListener('click', async () => {
  const checkboxes = document.querySelectorAll('.form-check-input:checked');
  const updates = [];
  
  if (checkboxes.length === 0) {
    alert("Güncellenecek paket seçmediniz!");
    return;
  }

  for (const checkbox of checkboxes) {
    const packageId = checkbox.id.replace('checkbox-', '');
    const packageRow = checkbox.closest('tr');
    const projectName = packageRow.querySelector('td:nth-child(1)').textContent.trim();
    const version = prompt(`Güncellemek istediğiniz versiyonu girin (Mevcut: ${packageRow.querySelector('td:nth-child(3)').textContent.trim()}):`, packageRow.querySelector('td:nth-child(3)').textContent.trim());

    if (version) {
      updates.push({ projectName, packageId, version });
    }
  }

  try {
    for (const update of updates) {
      await updatePackage(update.packageId, update.projectName, update.version);
    }
    alert('Paket güncellemeleri başarıyla gerçekleştirildi.');
    fetchPackages(); 
  } catch (error) {
    alert(error.message);
    console.error('Güncelleme işlemi sırasında hata oluştu:', error);
  }
});

document.getElementById('packages-button').addEventListener('click', async () => {
  const checkboxes = document.querySelectorAll('.form-check-input:checked');
  
  if (checkboxes.length === 0) {
    alert("Güncellenecek paket seçmediniz!");
    return;
  }

  const version = prompt("Güncellemek istediğiniz ortak versiyonu girin:");
  if (!version) {
    alert("Lütfen geçerli bir versiyon girin.");
    return;
  }

  const updates = [];

  checkboxes.forEach(checkbox => {
    const packageId = checkbox.id.replace('checkbox-', '');
    const packageRow = checkbox.closest('tr');
    const projectName = packageRow.querySelector('td:nth-child(1)').textContent.trim();
    updates.push({ projectName, packageId, version });
  });

  try {
    for (const update of updates) {
      await updatePackage(update.packageId, update.projectName, update.version);
    }
    alert('Seçilen paketler başarıyla güncellendi.');
    fetchPackages(); 
  } catch (error) {
    alert(error.message);
    console.error('Toplu güncelleme işlemi sırasında hata oluştu:', error);
  }
});