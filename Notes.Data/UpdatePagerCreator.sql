update np 

set np.Creator = nf.Creator

from [dbo].[NotesPage] np

inner join [dbo].[NotesForCourse] nf on nf.PageId = np.Id
